using System;
using GreenFever.Invoice.Client.Enums;

namespace GreenFever.Invoices.Client.Requests
{
    internal class BatchInvoiceProcessingRequest
    {
        public InvoiceType InvoiceType { get; set; }

        public DateTime RunDate { get; set; }
    }
}
