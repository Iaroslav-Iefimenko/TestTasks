using System;
using GreenFever.Invoice.Dto.Enums;
using GreenFever.Invoice.Dto.Objects;

namespace GreenFever.Invoice.Dal.Interfaces
{
    public interface IInvoicesProcessingRepository
    {
        // pckg_facturatie.ep_get_aknum_factrunvoorschot
        int GetTotalInvoicesCountForInvoiceType();

        // pckg_facturatie_afrekening.ep_get_aknum_factrunafrekening
        int GetTotalInvoicesCountForSettlementType();

        // pckg_facturatierun.ep_is_partijfrun_beschikbaar
        // batchRunId is used in DB functions only
        bool IsBatchInvoicesProcessingRunAvailable();

        // pckg_facturatierun.ep_start_partijfacturatierun
        // returns batchRunId
        int StartBatchInvoiceProcessingRun(
            InvoiceType invoicesType,
            DateTime runDate,
            int generalItemsCount);

        // pckg_facturatierun.ep_finish_partijfacturatierun
        bool FinishBatchInvoiceProcessingRun(int batchRunId);

        // pckg_facturatierun.ep_startfacturatierun
        InvoicesProcessingResult ExecuteBatchInvoicesProcessingRun(
            InvoiceType invoicesType,
            DateTime runDate,
            int batchRunId,
            int pageNum,
            int pageSize);

        // pckg_facturatierun.ep_partijfrun_percent_info
        ProcessingPercentageInfo GetBatchProcessingPercentageInfo(int batchRunId);

        // TO-DO: Get active batchId or 0 if acrive batch doesn't exist
        int GetActiveBatchRunId();

        // pckg_facturatierun.ep_checkActiefFacturatierun
        // clientId might be 0 for check of all single runs for all clients
        bool IsSingleInvoiceProcessingRunAvailable(int clientId = 0);

        // pckg_facturatierun.ep_MaakNieuweAfrekenFactuur
        InvoicesProcessingResult ExecuteSingleInvoiceProcessingRunForSettlementType(int clientId, DateTime runDate);

        // pckg_facturatierun.ep_MaakNieuweVoorschotFactuur
        InvoicesProcessingResult ExecuteSingleInvoiceProcessingRunForInvoiceType(int clientId, DateTime runDate);

        // pckg_facturatierun.pckg_facturatierun.ep_AantalManuele
        int GetCountOfInvoicesForManualGrouping();

        // pckg_facturatierun.ep_GroepeerManuele
        bool ExecuteManualInvoiceGrouping(DateTime runDate);
    }
}
