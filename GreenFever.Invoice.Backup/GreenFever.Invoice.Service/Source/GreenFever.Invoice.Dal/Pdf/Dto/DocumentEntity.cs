namespace GreenFever.Invoice.Dal.Pdf.Dto
{
    public class DocumentEntity
    {
        public string LanguageCode { get; set; } = LanguageCodes.NlBe;

        // RegioAfkorting
        public string RegionAbbreviation { get; set; } = string.Empty;
    }
}
