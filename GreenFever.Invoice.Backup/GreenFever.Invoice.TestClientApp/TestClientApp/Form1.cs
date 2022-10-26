using System;
using System.Windows.Forms;
using GreenFever.Invoice.Client.Enums;
using GreenFever.Invoices.Client;
using GreenFever.Invoices.Client.Dto;
using GreenFever.Invoices.Client.EventArgs;

namespace TestClientApp
{
    public partial class Form1 : Form
    {
        private const string PercentageInfoMessage = "{0} from {1} invoices processed ({2}%)";
        private const int ClientId = 2032426;

        private InvoicesProcessingClient processingClient;        

        public Form1()
        {
            this.InitializeComponent();

            this.processingClient = new InvoicesProcessingClient("https://localhost:44383/", ClientId);
            this.processingClient.BatchInvoicesProcessingStarted += this.OnBatchStartReceived;
            this.processingClient.BatchProcessingPercentageInfoReceived += this.OnPercentageInfoReceived;
            this.processingClient.BatchInvoicesProcessingFinished += this.OnBatchFinishReceived;
            this.processingClient.SingleInvoiceProcessingStarted += this.OnSingleStartReceived;
            this.processingClient.SingleInvoiceProcessingFinished += this.OnSingleFinishReceived;

            this.pbInvoices.Visible = false;
            this.lblPercentInfo.Visible = false;
        }

        private async void btnRunBatch_Click(object sender, System.EventArgs e)
        {
            try
            {
                var res = await this.processingClient.ExecuteBatchInvoicesProcessing(InvoiceType.Afrekening, DateTime.Now);
                string message;
                if (res.ContainsErrors)
                {
                    message = res.PdfErrors[0];
                }
                else
                {
                    message = "Batch processing is finished successfully";
                }

                MessageBox.Show(message);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async void btnRunSingle_Click(object sender, EventArgs e)
        {
            try
            {
                var res = await this.processingClient.ExecuteSingleInvoiceProcessing(InvoiceType.Afrekening, DateTime.Now);
                string message;
                if (res.ContainsErrors)
                {
                    message = res.PdfErrors[0];
                }
                else
                {
                    message = "Single processing is finished successfully";
                }

                MessageBox.Show(message);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async void btnExecuteGrouping_Click(object sender, EventArgs e)
        {
            try
            {
                var cnt = await this.processingClient.GetCountOfInvoicesForGrouping();
                if (cnt == 0)
                {
                    MessageBox.Show("No items for manual grouping");
                    return;
                }

                var result = await this.processingClient.GroupInvoices(DateTime.Now);

                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("Manual grouping is executed successfully");
                }
                else
                {
                    MessageBox.Show(result);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void OnBatchStartReceived(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                // Running on the UI thread
                this.pbInvoices.Value = 0;
                this.pbInvoices.Visible = true;

                this.btnRunBatch.Text = "Try start again...";
                this.btnRunSingle.Text = "Try start...";
            });
        }

        private void OnPercentageInfoReceived(object sender, EventArgs<ProcessingPercentageInfo> e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                // Running on the UI thread
                this.lblPercentInfo.Visible = true;
                this.lblPercentInfo.Text = string.Format(
                    PercentageInfoMessage,
                    e.Value.ProcessedItemsCount,
                    e.Value.TotalItemsCount,
                    e.Value.Percent.ToString("0.##"));

                // this.pbInvoices.Value = (int)e.Value.Percent;
                // this.upbInvoices.Value = (int)e.Value.Percent;
                if (this.pbInvoices.Value + 5 <= this.pbInvoices.Maximum)
                {
                    this.pbInvoices.Value += 5;
                }
            });
        }

        private void OnBatchFinishReceived(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                // Running on the UI thread
                this.lblPercentInfo.Visible = false;
                this.pbInvoices.Visible = false;
                this.btnRunBatch.Text = "Start batch processing";
                this.btnRunSingle.Text = "Start single processing";
            });
        }

        private void OnSingleStartReceived(object sender, EventArgs<int> e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                // Running on the UI thread
                this.pbInvoices.Value = 0;
                this.pbInvoices.Style = ProgressBarStyle.Marquee;
                this.pbInvoices.Visible = true;
                
                this.btnRunBatch.Text = "Try start...";
                this.btnRunSingle.Text = "Try start again...";
            });
        }

        private void OnSingleFinishReceived(object sender, EventArgs<int> e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                // Running on the UI thread
                this.pbInvoices.Visible = false;
                this.pbInvoices.Style = ProgressBarStyle.Continuous;
                this.btnRunBatch.Text = "Start batch processing";
                this.btnRunSingle.Text = "Start single processing";
            });
        }
    }
}
