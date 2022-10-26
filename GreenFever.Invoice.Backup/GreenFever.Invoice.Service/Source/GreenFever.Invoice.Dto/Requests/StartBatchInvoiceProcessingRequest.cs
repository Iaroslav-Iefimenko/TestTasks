using System;
using GreenFever.Invoice.Dto.Enums;

namespace GreenFever.Invoice.Dto.Requests
{
    public class StartBatchInvoiceProcessingRequest
    {
        public InvoiceType InvoiceType { get; set; }

        public DateTime RunDate { get; set; }
    }
}
