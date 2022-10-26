using System;
using GreenFever.Invoice.BusinessLogic.Interfaces;
using GreenFever.Invoice.Dal.Interfaces;
using GreenFever.Invoice.Dto.Enums;
using GreenFever.Invoice.Dto.Objects;
using GreenFever.Invoice.Utils.Configuration;
using Microsoft.Extensions.Options;

namespace GreenFever.Invoice.BusinessLogic.Services
{
    public class InvoicesProcessingService : IInvoicesProcessingService
    {
        private readonly IInvoicesProcessingRepository processingRepository;
        private readonly InvoiceProcessingSetting processingSettings;

        public InvoicesProcessingService(
            IInvoicesProcessingRepository processingRepository,
            IOptions<InvoiceProcessingSetting> processingSettings)
        {
            this.processingRepository = processingRepository ?? throw new ArgumentNullException(nameof(processingRepository));
            this.processingSettings = processingSettings?.Value ?? throw new ArgumentNullException(nameof(processingSettings));
        }

        #region Batch processing

        public InvoicesProcessingResult ExecuteBatchInvoicesProcessingRun(InvoiceType invoicesType, DateTime runDate)
        {
            var result = new InvoicesProcessingResult();
            int batchRunId = 0;

            try
            {
                if (invoicesType == InvoiceType.Commission)
                {
                    result.ContainsErrors = true;
                    result.PdfErrors.Add("Commission invoices cannot be processed");
                    return result;
                }

                if (!this.processingRepository.IsBatchInvoicesProcessingRunAvailable())
                {
                    result.ContainsErrors = true;
                    result.PdfErrors.Add("Batch processing run is already started");
                    return result;
                }

                int totalItemsCount = this.GetTotalCountByInvoiceType(invoicesType);
                if (totalItemsCount == 0)
                {
                    result.ContainsErrors = true;
                    result.PdfErrors.Add("No invoices for processing");
                    return result;
                }

                batchRunId = this.processingRepository.StartBatchInvoiceProcessingRun(invoicesType, runDate, totalItemsCount);

                int pageSize = this.processingSettings.BatchSize;
                int pagesCount = (totalItemsCount / pageSize) + 1;

                // batch processing
                for (int pageNum = 0; pageNum < pagesCount; pageNum++)
                {
                    var partialResult = this.processingRepository.ExecuteBatchInvoicesProcessingRun(
                        invoicesType,
                        runDate,
                        batchRunId,
                        pageNum,
                        pageSize);

                    result.ContainsErrors = result.ContainsErrors || partialResult.ContainsErrors;
                    result.EANsWithoutSLPcode.AddRange(partialResult.EANsWithoutSLPcode);
                    result.EANsDouble.AddRange(partialResult.EANsDouble);
                    result.SLPcodesMissing.AddRange(partialResult.SLPcodesMissing);
                    result.Invoices.AddRange(partialResult.Invoices);
                    result.PdfErrors.AddRange(partialResult.PdfErrors);
                }
            }
            catch
            {
                // TO-DO: add logging and error information                
            }
            finally
            {
                if (batchRunId != 0)
                {
                    this.processingRepository.FinishBatchInvoiceProcessingRun(batchRunId);
                }
            }

            return result;
        }

        public bool IsBatchInvoicesProcessingRunAvailable()
        {
            return this.processingRepository.IsBatchInvoicesProcessingRunAvailable();
        }

        public ProcessingPercentageInfo GetBatchProcessingPercentageInfo(int batchRunId)
        {
            return this.processingRepository.GetBatchProcessingPercentageInfo(batchRunId);
        }

        public int GetActiveBatchRunId()
        {
            return this.processingRepository.GetActiveBatchRunId();
        }

        #endregion

        #region Single processing

        public InvoicesProcessingResult ExecuteSingleInvoiceProcessingRun(InvoiceType invoicesType, int clientId, DateTime runDate)
        {
            var result = new InvoicesProcessingResult { ClientId = clientId };

            if (!this.processingRepository.IsBatchInvoicesProcessingRunAvailable())
            {
                result.ContainsErrors = true;
                result.PdfErrors.Add("Batch processing run is already started");
                return result;
            }

            if (!this.processingRepository.IsSingleInvoiceProcessingRunAvailable(clientId))
            {
                result.ContainsErrors = true;
                result.PdfErrors.Add("Single processing run is already started for this client");
                return result;
            }

            try
            {
                switch (invoicesType)
                {
                    case InvoiceType.Invoice:
                    {
                        result = this.processingRepository.ExecuteSingleInvoiceProcessingRunForInvoiceType(clientId, runDate);
                        break;
                    }

                    case InvoiceType.Settlement:
                    {
                        result = this.processingRepository.ExecuteSingleInvoiceProcessingRunForSettlementType(clientId, runDate);
                        break;
                    }
                }
                
                result.ClientId = clientId;                
            }
            catch
            {
                // TO-DO: add logging and error information                
            }

            return result;
        }

        public bool IsSingleInvoiceProcessingRunAvailable(int clientId)
        {
            return this.processingRepository.IsSingleInvoiceProcessingRunAvailable(clientId);
        }

        #endregion

        #region Manual Grouping

        public string ExecuteManualInvoiceGrouping(DateTime runDate)
        {
            if (!this.processingRepository.IsBatchInvoicesProcessingRunAvailable())
            {
                return "Invoice processing run in progress...";
            }

            try
            {
                this.processingRepository.ExecuteManualInvoiceGrouping(runDate);
            }
            catch
            {
                // TO-DO: add logging and error information                
            }

            return string.Empty;
        }

        public int GetCountOfInvoicesForManualGrouping()
        {
            return this.processingRepository.GetCountOfInvoicesForManualGrouping();
        }

        #endregion

        private int GetTotalCountByInvoiceType(InvoiceType invoicesType)
        {
            int totalItemsCount;
            switch (invoicesType)
            {
                case InvoiceType.Invoice:
                {
                    totalItemsCount = this.processingRepository.GetTotalInvoicesCountForInvoiceType();
                    break;
                }

                case InvoiceType.Settlement:
                {
                    totalItemsCount = this.processingRepository.GetTotalInvoicesCountForSettlementType();
                    break;
                }

                default:
                {
                    totalItemsCount = 0;
                    break;
                }
            }

            return totalItemsCount;
        }
    }
}
