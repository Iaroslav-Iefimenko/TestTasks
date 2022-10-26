using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GreenFever.Invoice.Dal.Dto;
using GreenFever.Invoice.Dal.Interfaces;
using GreenFever.Invoice.Dal.Pdf.Dto;
using GreenFever.Invoice.Dto.Enums;
using GreenFever.Invoice.Dto.Objects;
using GreenFever.Invoice.Utils.Configuration;
using Microsoft.Extensions.Options;

namespace GreenFever.Invoice.Dal.Repositories
{
    public class InvoicesProcessingRepository : IInvoicesProcessingRepository
    {
        // these variables and function realizations are added for prototyping only
        private static bool isBatchStarted;
        private static bool isSingleStarted;
        private int invoiceCount = 51423;
        private int settlementCount = 63524;
        private int runId = 134;

        private ConnectionStrings connectionStrings;

        public InvoicesProcessingRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            this.connectionStrings = connectionStrings?.Value ?? throw new ArgumentNullException(nameof(connectionStrings));
        }

        #region Batch Invoices Processing

        public InvoicesProcessingResult ExecuteBatchInvoicesProcessingRun(
            InvoiceType invoicesType,
            DateTime runDate,
            int batchRunId,
            int pageNum,
            int pageSize)
        {
            Thread.Sleep(500);

            // TO-DO: following variables are get from pckg_facturatierun.ep_startfacturatierun
            List<InvoiceError> errorsList = new List<InvoiceError>();
            int currentRunId = 0;

            var result = this.ProcessRunResult(currentRunId, errorsList);

            return result;
        }

        public int GetTotalInvoicesCountForSettlementType()
        {
            return this.invoiceCount;
        }

        public int GetTotalInvoicesCountForInvoiceType()
        {
            return this.settlementCount;
        }

        public bool IsBatchInvoicesProcessingRunAvailable()
        {
            return !InvoicesProcessingRepository.isBatchStarted && !InvoicesProcessingRepository.isSingleStarted;
        }

        public int StartBatchInvoiceProcessingRun(InvoiceType invoicesType, DateTime runDate, int generalItemsCount)
        {
            isBatchStarted = true;
            return this.runId;
        }

        public bool FinishBatchInvoiceProcessingRun(int batchRunId)
        {
            isBatchStarted = false;
            return true;
        }

        public ProcessingPercentageInfo GetBatchProcessingPercentageInfo(int batchRunId)
        {
            return new ProcessingPercentageInfo
            {
                Percent = 50,
                ProcessedItemsCount = 50,
                TotalItemsCount = 100
            };
        }

        public int GetActiveBatchRunId()
        {
            return InvoicesProcessingRepository.isBatchStarted ? this.runId : 0;
        }

        #endregion

        #region Single Invoice Processing

        public bool IsSingleInvoiceProcessingRunAvailable(int clientId)
        {
            return !InvoicesProcessingRepository.isBatchStarted && !InvoicesProcessingRepository.isSingleStarted;
        }

        public InvoicesProcessingResult ExecuteSingleInvoiceProcessingRunForSettlementType(int clientId, DateTime runDate)
        {
            isSingleStarted = true;
            Thread.Sleep(5000);
            isSingleStarted = false;
            return new InvoicesProcessingResult { ResultCode = 1 };
        }

        public InvoicesProcessingResult ExecuteSingleInvoiceProcessingRunForInvoiceType(int clientId, DateTime runDate)
        {
            isSingleStarted = true;
            Thread.Sleep(5000);
            isSingleStarted = false;
            return new InvoicesProcessingResult { ResultCode = 1 };
        }

        #endregion

        #region Manual Invoices Grouping

        public int GetCountOfInvoicesForManualGrouping()
        {
            return 53;
        }

        public bool ExecuteManualInvoiceGrouping(DateTime runDate)
        {
            return true;
        }

        #endregion

        private InvoicesProcessingResult ProcessRunResult(int currentRunId, List<InvoiceError> errList)
        {
            var res = new InvoicesProcessingResult();

            res.ContainsErrors = errList.Any();
            res.EANsWithoutSLPcode.AddRange(from err in errList where !string.IsNullOrEmpty(err.EanSlp) select err.EanSlp);
            res.EANsDouble.AddRange(from err in errList where !string.IsNullOrEmpty(err.EanDouble) select err.EanDouble);
            res.SLPcodesMissing.AddRange(
                from err in errList
                where !string.IsNullOrEmpty(err.SlpCode)
                select string.Format("{0} - {1}", err.SlpCode, err.ErrorDate.ToString("d/M/yy")));
            res.Invoices.AddRange(from err in errList where !string.IsNullOrEmpty(err.Invoice) select err.Invoice);

            var saleLidersList = this.GetSaleLidersListByRunId(currentRunId);

            foreach (var sl in saleLidersList)
            {
                // voorschotfactuurDocType
                var docTypeItem = this.GetSaleLiderDocumentTypesBySaleLiderId(sl.Id);
                
                /*
                Try
                    Dim entity = mailPdfProcessor.GetPdfDocumentEntity(voorschotfactuurDocType)

                    If mailPdfProcessor.PDFContainsError(voorschotfactuurDocType, entity) Then
                        facerrors.pdfError.Add(row("KLANTNUMMER"))
                    End If
                Catch ex As Exception
                    facerrors.pdfError.Add(row("KLANTNUMMER"))
                End Try*/
            }

            return res;
        }

        private List<SaleLider> GetSaleLidersListByRunId(int runId)
        {
            // PCKG_FACTOVERZICHT.EP_GETFACTURENINFACTURATIERUN
            return new List<SaleLider>();
        }

        private CommunicationItemDocumentType GetSaleLiderDocumentTypesBySaleLiderId(int saleLiderId)
        {
            // PCKG_EMAILPDFGENERATIE.EP_GETDOCTYPEVERKOOPKOP
            return new CommunicationItemDocumentType();
        }
    }
}
