using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using GreenFever.Invoice.Client.Enums;
using GreenFever.Invoice.Dto.Responses;
using GreenFever.Invoices.Client.Dto;
using GreenFever.Invoices.Client.EventArgs;
using GreenFever.Invoices.Client.Requests;

namespace GreenFever.Invoices.Client
{
    public class InvoicesProcessingClient : IDisposable
    {
        private readonly HttpClient client;
        private readonly int timeout;
        private readonly int clientId;
        private System.Timers.Timer timer;

        // multithreaded variables
        private int isBatchRunAvailable;
        private int isSingleRunAvailable;

        public InvoicesProcessingClient(string baseComponentAddress, int clientId = 0, int timeout = 3000)
        {
            this.timeout = timeout;
            this.clientId = clientId;

            // create connection to controller
            this.client = new HttpClient
            {
                BaseAddress = new Uri(baseComponentAddress)
            };
            this.client.Timeout = TimeSpan.FromMinutes(30);
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // create timer for runs state pulling
            this.isBatchRunAvailable = 1;
            this.isSingleRunAvailable = 1;

            this.timer = new System.Timers.Timer(timeout);
            this.timer.Elapsed += this.CheckRunsState;
            this.timer.AutoReset = true;
            this.timer.Enabled = true;
        }

        public event EventHandler<EventArgs<int>> SingleInvoiceProcessingStarted;

        public event EventHandler<EventArgs<int>> SingleInvoiceProcessingFinished;

        public event EventHandler<System.EventArgs> BatchInvoicesProcessingStarted;

        public event EventHandler<System.EventArgs> BatchInvoicesProcessingFinished;

        public event EventHandler<EventArgs<ProcessingPercentageInfo>> BatchProcessingPercentageInfoReceived;

        public void Dispose()
        {
            if (this.timer != null)
            {
                this.timer.Stop();              
            }
        }

        public async Task<InvoicesProcessingResult> ExecuteBatchInvoicesProcessing(InvoiceType invoiceType, DateTime runDate)
        {
            bool isBatchRunAvailableNew = await this.IsBatchInvoicesProcessingAvailable();

            if (!isBatchRunAvailableNew)
            {
                throw new Exception("Batch run is not available");
            }

            this.BatchInvoicesProcessingStarted?.Invoke(this, new System.EventArgs());
            var result = await this.ExecuteBatchInvoicesProcessingRun(invoiceType, runDate);
            this.BatchInvoicesProcessingFinished?.Invoke(this, new System.EventArgs());
            return result;
        }

        public async Task<InvoicesProcessingResult> ExecuteSingleInvoiceProcessing(InvoiceType invoiceType, DateTime runDate)
        {
            bool isSingleRunAvailableNew = await this.IsSingleInvoiceProcessingAvailable(this.clientId);

            if (!isSingleRunAvailableNew)
            {
                throw new Exception("Single run is not available for this client");
            }

            this.SingleInvoiceProcessingStarted?.Invoke(this, new EventArgs<int>(this.clientId));
            var result = await this.ExecuteSingleInvoiceProcessingRun(invoiceType, this.clientId, runDate);
            this.SingleInvoiceProcessingFinished?.Invoke(this, new EventArgs<int>(this.clientId));
            return result;
        }

        public async Task<string> GroupInvoices(DateTime runDate)
        {
            return await this.ExecuteManualInvoicesGrouping(runDate);
        }

        public async Task<int> GetCountOfInvoicesForGrouping()
        {
            return await this.GetCountOfInvoicesForManualGrouping();
        }

        #region Timer functions

        private async void CheckRunsState(object source, ElapsedEventArgs e)
        {
            try
            {
                int batchRunId = await this.GetActiveBatchRunId();

                if (batchRunId != 0)
                {
                    if (this.isBatchRunAvailable == 1)
                    {
                        // batch run is started
                        this.BatchInvoicesProcessingStarted?.Invoke(this, new System.EventArgs());
                    }

                    if (this.isBatchRunAvailable == 0)
                    {
                        // batch run in progress
                        var info = await this.GetBatchProcessingPercentageInfo(batchRunId);
                        this.BatchProcessingPercentageInfoReceived?.Invoke(this, new EventArgs<ProcessingPercentageInfo>(info));
                    }
                }
                else
                {
                    if (this.isBatchRunAvailable == 0)
                    {
                        // batch run is finished
                        this.BatchInvoicesProcessingFinished?.Invoke(this, new System.EventArgs());
                    }

                    // check single run
                    bool isSingleRunAvailableNew = await this.IsSingleInvoiceProcessingAvailable(this.clientId);

                    if (!isSingleRunAvailableNew && this.isSingleRunAvailable == 1)
                    {
                        // single run is started
                        this.SingleInvoiceProcessingStarted?.Invoke(this, new EventArgs<int>(this.clientId));
                    }

                    if (isSingleRunAvailableNew && this.isSingleRunAvailable == 0)
                    {
                        // single run is finished
                        this.SingleInvoiceProcessingFinished?.Invoke(this, new EventArgs<int>(this.clientId));
                    }

                    Interlocked.Exchange(ref this.isSingleRunAvailable, isSingleRunAvailableNew ? 1 : 0);
                }

                Interlocked.Exchange(ref this.isBatchRunAvailable, batchRunId == 0 ? 1 : 0);
            }
            catch (Exception e)
            {
                // TO-DO: ADD ERROR LOGGING
            }
        }

        #endregion

        #region Service methods

        #region batch processing

        private async Task<InvoicesProcessingResult> ExecuteBatchInvoicesProcessingRun(
            InvoiceType invoiceType,
            DateTime runDate)
        {
            var req = new BatchInvoiceProcessingRequest
            {
                InvoiceType = invoiceType,
                RunDate = runDate
            };

            HttpResponseMessage response = await this.client.PostAsJsonAsync("api//InvoicesProcessing//execute-batch", req);
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsAsync<GenericResponse<InvoicesProcessingResult>>();
                return resp.Data;
            }

            return null;
        }

        private async Task<bool> IsBatchInvoicesProcessingAvailable()
        {
            HttpResponseMessage response = await this.client.PostAsync("api//InvoicesProcessing//is-batch-available", null);
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsAsync<GenericResponse<bool>>();
                return resp.Data;
            }

            return false;
        }

        private async Task<ProcessingPercentageInfo> GetBatchProcessingPercentageInfo(int batchRunId)
        {
            var req = new GetBatchPercentageInfoRequest { BatchRunId = batchRunId };

            HttpResponseMessage response = await this.client.PostAsJsonAsync("api//InvoicesProcessing//get-batch-percentage-info", req);
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsAsync<GenericResponse<ProcessingPercentageInfo>>();
                return resp.Data;
            }

            return null;
        }

        private async Task<int> GetActiveBatchRunId()
        {
            HttpResponseMessage response = await this.client.PostAsync("api//InvoicesProcessing//get-active-batch-id", null);
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsAsync<GenericResponse<int>>();
                return resp.Data;
            }

            return 0;
        }

        #endregion

        #region Single processing

        private async Task<InvoicesProcessingResult> ExecuteSingleInvoiceProcessingRun(
            InvoiceType invoiceType,
            int clientId,
            DateTime runDate)
        {
            var req = new SingleInvoiceProcessingRequest
            {
                InvoiceType = invoiceType,
                ClientId = clientId,
                RunDate = runDate
            };

            HttpResponseMessage response = await this.client.PostAsJsonAsync("api//InvoicesProcessing//execute-single", req);
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsAsync<GenericResponse<InvoicesProcessingResult>>();
                return resp.Data;
            }

            return null;
        }

        private async Task<bool> IsSingleInvoiceProcessingAvailable(int clientId)
        {
            var req = new IsSingleInvoiceProcessingAvailableRequest { ClientId = clientId };

            HttpResponseMessage response = await this.client.PostAsJsonAsync("api//InvoicesProcessing//is-single-available", req);
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsAsync<GenericResponse<bool>>();
                return resp.Data;
            }

            return false;
        }

        #endregion

        #region Manual grouping

        private async Task<string> ExecuteManualInvoicesGrouping(DateTime runDate)
        {
            var req = new ExecuteManualInvoiceGroupingRequest { RunDate = runDate };

            HttpResponseMessage response = await this.client.PostAsJsonAsync("api//InvoicesProcessing//group-invoices", req);
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsAsync<GenericResponse<string>>();
                return resp.Data;
            }

            return response.StatusCode.ToString();
        }

        private async Task<int> GetCountOfInvoicesForManualGrouping()
        {
            HttpResponseMessage response = await this.client.PostAsync("api//InvoicesProcessing//get-cnt-for-invoices-group", null);
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsAsync<GenericResponse<int>>();
                return resp.Data;
            }

            return 0;
        }

        #endregion

        #endregion
    }
}
