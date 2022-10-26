using System;
using System.Data;
using GreenFever.Invoice.Dal.Dto.Enums;
using GreenFever.Invoice.Dal.Utils;

namespace GreenFever.Invoice.Dal.Pdf.Dto
{
    /// <summary>
    /// Class that contains all data needed to create a PDf or email from a communication item.
    /// (Basically a combination of communication item and documenttype)
    /// </summary>
    /// <remarks></remarks>
    public class CommunicationItemDocumentType
    {
        /// <summary>
        /// testing
        /// </summary>
        /// <remarks></remarks>
        public CommunicationItemDocumentType()
        {
        }

        public CommunicationItemDocumentType(DataSet ds)
        {
            var dr = ds.Tables[0].Rows[0];

            int? val = DataSetUtilities.GetDataItemInteger(dr, "SEQCOMMUNICATIEITEM");
            this.CommunicationItemId = val.HasValue ? val.Value : -1;
            this.CommunicationTypeCode = DataSetUtilities.GetDataItemString(dr, "COMMUNICATIETYPECODE");
            this.PersonId = DataSetUtilities.GetDataItemInteger(dr, "SEQPERSOON");
            this.RequestId = DataSetUtilities.GetDataItemInteger(dr, "SEQAANVRAAG");
            this.SaleLiderId = DataSetUtilities.GetDataItemInteger(dr, "SEQVERKOOPKOP");
            this.DocumentGuid = DataSetUtilities.GetDataItemString(dr, "DOCUMENTGUID");
            this.DocumentTypeCode = DataSetUtilities.GetDataItemString(dr, "DOCUMENTTYPECODE");
            this.PdfMainTemplate = DataSetUtilities.GetDataItemString(dr, "PDFMAINTEMPLATE");
            this.PdfHeaderTemplate = DataSetUtilities.GetDataItemString(dr, "PDFHEADERTEMPLATE");
            val = DataSetUtilities.GetDataItemInteger(dr, "PDFHEADERHEIGHT");
            this.PdfHeaderHeight = val.HasValue ? val.Value : -1;
            this.PdfFooterTemplate = DataSetUtilities.GetDataItemString(dr, "PDFFOOTERTEMPLATE");
            val = DataSetUtilities.GetDataItemInteger(dr, "PDFFOOTERHEIGHT");
            this.PdfFooterHeight = val.HasValue ? val.Value : -1;
            this.PdfPageNumberTemplate = DataSetUtilities.GetDataItemString(dr, "PDFPAGENUMBERTEMPLATE");
            this.PdfDisplayHeaderFirstPage = DataSetUtilities.GetDataItemBoolean(dr, "PDFDISPLAYHEADERFIRSTPAG");
            this.PdfDisplayFooterFirstPage = DataSetUtilities.GetDataItemBoolean(dr, "PDFDISPLAYFOOTERFIRSTPAG");
            this.EmailMainTemplate = DataSetUtilities.GetDataItemString(dr, "EMAILMAINTEMPLATE");
            this.ClientNumber = DataSetUtilities.GetDataItemString(dr, "KLANTNUMMER");
            DateTime? dval = DataSetUtilities.GetDataItemDateTime(dr, "CREDAT");
            this.CreateDate = dval.HasValue ? dval.Value : DateTime.MinValue;
            this.IsCopy = DataSetUtilities.GetDataItemBoolean(dr, "KOPIE");
            this.PersonNameCopy = DataSetUtilities.GetDataItemString(dr, "PERSOONNAAMKOPIE");
            this.CopyPersonContactId = DataSetUtilities.GetDataItemInteger(dr, "SEQKOPIEPERSOONCONTACT");
            this.NavRemindersHeaderNo = DataSetUtilities.GetDataItemString(dr, "NAV_AANMANINGENHEADER_NO");
            this.CustomHeader = null;
            this.ForRicohClickAndPost = false;
            this.DetailCalculationId = DataSetUtilities.GetDataItemInteger(dr, "SEQDETAILBEREKENING");
        }

        public int CommunicationItemId { get; set; }

        public string CommunicationTypeCode { get; set; }

        public int? PersonId { get; set; }

        // SeqAanvraag
        public int? RequestId { get; set; }

        // SeqVerkoopkop
        public int? SaleLiderId { get; set; }

        public string DocumentGuid { get; set; }

        public string DocumentTypeCode { get; set; }

        public string PdfMainTemplate { get; set; }

        public string PdfHeaderTemplate { get; set; }

        public int PdfHeaderHeight { get; set; }

        public string PdfFooterTemplate { get; set; }

        public int PdfFooterHeight { get; set; }

        public string PdfPageNumberTemplate { get; set; }

        public bool PdfDisplayHeaderFirstPage { get; set; }

        public bool PdfDisplayFooterFirstPage { get; set; }

        public string EmailMainTemplate { get; set; }
        
        public string ClientNumber { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsCopy { get; set; }
        
        public string PersonNameCopy { get; set; }

        // Note: PersoonContact <> Persoon
        public int? CopyPersonContactId { get; set; }

        public string NavRemindersHeaderNo { get; set; }

        public string CustomHeader { get; set; }

        // SeqDetailBerekening
        public int? DetailCalculationId { get; set; }

        public bool ForRicohClickAndPost { get; set; }
        
        /// <summary>
        /// Returns the value of SeqKopiePersoonContact or -1 if SeqKopiePersoonContact has no value
        /// In Oracle -1 is used to indicate that an integer is NULL
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int CopyPersonContactIdOrOracleNULL
        {
            get => this.CopyPersonContactId.HasValue ? this.CopyPersonContactId.Value : -1;
        }

        public DocumentTypeCodes DocumentType
        {
            get => (DocumentTypeCodes)Enum.Parse(typeof(DocumentTypeCodes), this.DocumentTypeCode);
        }

        public CommunicationTypeCodes CommunicationType
        {
            get => (CommunicationTypeCodes)Enum.Parse(typeof(CommunicationTypeCodes), this.CommunicationTypeCode);
        }

        public bool HasPdf
        {
            get => !string.IsNullOrEmpty(this.PdfMainTemplate);
        }

        public bool HasPdfHeader
        {
            get => !string.IsNullOrEmpty(this.PdfHeaderTemplate) && this.PdfHeaderHeight > 0;
        }

        public bool HasPdfFooter
        { 
            get => !string.IsNullOrEmpty(this.PdfFooterTemplate) && this.PdfFooterHeight > 0;
        }

        public bool HasPdfPageNumbers
        {
            get => !string.IsNullOrEmpty(this.PdfPageNumberTemplate);
        }

        public bool HasEmail
        { 
            get => !string.IsNullOrEmpty(this.EmailMainTemplate);
        }        
    }
}
