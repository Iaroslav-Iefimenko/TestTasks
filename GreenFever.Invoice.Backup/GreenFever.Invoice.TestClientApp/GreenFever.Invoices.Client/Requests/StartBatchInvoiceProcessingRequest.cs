using System;
using GreenFever.Invoice.Client.Enums;

namespace GreenFever.Invoices.Client.Requests
{
    internal class StartBatchInvoiceProcessingRequest
    {
        public InvoiceType InvoiceType { get; set; }

        public DateTime RunDate { get; set; }
    }
}
