﻿using System.Collections.Generic;

namespace GreenFever.Invoices.Client.Dto
{
    public class InvoicesProcessingResult
    {
        public InvoicesProcessingResult()
        {
            this.EANsWithoutSLPcode = new List<string>();
            this.EANsDouble = new List<string>();
            this.SLPcodesMissing = new List<string>();
            this.Advances = new List<string>();
            this.PdfErrors = new List<string>();
        }

        // 0 for batch processing
        public int ClientId { get; set; }

        public bool ContainsErrors { get; set; }

        public List<string> EANsWithoutSLPcode { get; set; }

        public List<string> EANsDouble { get; set; }

        public List<string> SLPcodesMissing { get; set; }

        // Voorschotten
        public List<string> Advances { get; set; }

        public List<string> PdfErrors { get; set; }
    }
}
