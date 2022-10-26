using System;

namespace GreenFever.Invoice.Dal.Dto
{
    // TYPRECFACTURATIE_ERROR
    public class InvoiceError
    {
        public string EanSlp { get; set; }

        // ean_dubbel
        public string EanDouble { get; set; }

        public string SlpCode { get; set; }

        // datum
        public DateTime ErrorDate { get; set; }

        // Voorschot
        public string Invoice { get; set; }
    }
}
