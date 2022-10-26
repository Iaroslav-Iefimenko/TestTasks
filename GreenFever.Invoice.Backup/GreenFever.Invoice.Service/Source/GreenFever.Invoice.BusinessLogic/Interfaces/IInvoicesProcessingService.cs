using System;
using GreenFever.Invoice.Dto.Enums;
using GreenFever.Invoice.Dto.Objects;

namespace GreenFever.Invoice.BusinessLogic.Interfaces
{
    public interface IInvoicesProcessingService
    {
        #region Batch processing

        InvoicesProcessingResult ExecuteBatchInvoicesProcessingRun(InvoiceType invoicesType, DateTime runDate);

        bool IsBatchInvoicesProcessingRunAvailable();

        ProcessingPercentageInfo GetBatchProcessingPercentageInfo(int batchRunId);

        int GetActiveBatchRunId();

        #endregion

        #region Single processing

        InvoicesProcessingResult ExecuteSingleInvoiceProcessingRun(InvoiceType invoicesType, int clientId, DateTime runDate);

        bool IsSingleInvoiceProcessingRunAvailable(int clientId);

        #endregion

        #region Manual Grouping

        string ExecuteManualInvoiceGrouping(DateTime runDate);

        int GetCountOfInvoicesForManualGrouping();

        #endregion
    }
}
