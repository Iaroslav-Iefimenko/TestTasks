using System;
using GreenFever.Invoice.Client.Enums;

namespace GreenFever.Invoices.Client.Requests
{
    internal class SingleInvoiceProcessingRequest
    {
        public InvoiceType InvoiceType { get; set; }

        public DateTime RunDate { get; set; }

        public int ClientId { get; set; }
    }
}
