using System;
using GreenFever.Invoice.Dto.Enums;

namespace GreenFever.Invoice.Dto.Requests
{
    public class SingleInvoiceProcessingRequest
    {
        public InvoiceType InvoiceType { get; set; }

        public DateTime RunDate { get; set; }

        public int ClientId { get; set; }
    }
}
