using System;
using System.Linq;
using System.Reflection;
using GreenFever.Invoice.Dal.Pdf.Dto;

namespace GreenFever.Invoice.Dal.Pdf
{
    internal class PdfProcessor
    {
        /// <summary>
        /// Makes the html content like it does for the pdf. When a error occur, the will return TRUE
        /// </summary>
        /// <param name="commItem"></param>
        /// <param name="entity"></param>
        /// <returns>true when contains a err</returns>
        /// <remarks></remarks>
        public bool PDFContainsError(CommunicationItemDocumentType commItem, DocumentEntity entity)
        {
            var templateFactory = new TemplateFactory();

            // Get Contents for each part
            var html = this.GetTemplateHTML(commItem.PdfHeaderTemplate, templateFactory, entity) +
                       this.GetTemplateHTML(commItem.PdfMainTemplate, templateFactory, entity) +
                       this.GetTemplateHTML(commItem.PdfFooterTemplate, templateFactory, entity);

            // Check Content for empty fields
            if (html.Contains("[[txt:") || html.Contains("[[code:") || html.Contains("Decentrale productie: kVA"))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get the PDF DocumentEntity for the given CommunicatieItemDocumenttype.
        /// The type of the entity is based on the documenttype.
        /// </summary>
        /// <param name="commItem"></param>
        /// <param name="body"></param>
        /// <returns>DocumentEntity</returns>
        /// <remarks>Probably there are better solutions that provide a way so you don't have to manually set each option in a switch case</remarks>
        public DocumentEntity GetPdfDocumentEntity(CommunicationItemDocumentType commItem, string body = "")
        {
            DocumentEntity docEntity = null;

            switch (commItem.DocumentTypeCode)
            {
                case "VOORSCHOTFACTUUR":
                {
                    docEntity = this.GetInvoicePdfEntities(commItem.SaleLiderId);
                    break;
                }

                case "VOORSCHOTFACTUUR_CREDIT":
                {
                    docEntity = this.GetInvoicePdfCreditNoteEntities(commItem.SaleLiderId);
                    break;
                }

                case "FACTUUR":
                {
                    docEntity = this.GetConsumptionPdfEntities(commItem.SaleLiderId);
                    break;
                }

                case "CREDITNOTA":
                {
                    docEntity = this.GetConsumptionPdfCreditNoteEntities(commItem.SaleLiderId);
                    break;
                }

                case "SLOTFACTUUR":
                {
                    docEntity = this.GetConsumptionPdfEntities(commItem.SaleLiderId);
                    break;
                }

                case "INJECTIEFACTUUR":
                {
                    docEntity = this.GetConsumptionPdfEntities(commItem.SaleLiderId);
                    break;
                }

                case "INJECTIESLOTFACTUUR":
                {
                    docEntity = this.GetConsumptionPdfEntities(commItem.SaleLiderId);
                    break;
                }

                case "CONTRACT":
                {
                    docEntity = this.GetContractPdfEntities(commItem.PersonId);
                    break;
                }

                case "WELKOMBRIEF":
                {
                    docEntity = this.GetContractPdfWelcomeLetterEntities(commItem.PersonId);
                    break;
                }

                case "DOMICILIERINGSAANVRAAG":
                {
                    docEntity = this.GetDomiciliationPdfEntities(commItem);
                    break;
                }

                case "AANMANING1":
                {
                    docEntity = this.GetReminderPdfEntities(commItem);
                    break;
                }

                case "DOMICILIERINGSHERINNERING":
                {
                    docEntity = this.GetReminderPdfEntities(commItem);
                    break;
                }

                case "INGEBREKESTELLING":
                {
                    docEntity = this.GetReminderPdfEntities(commItem);
                    break;
                }

                case "PRIJSCALCULATOR":
                {
                    docEntity = this.GetDetailCalculationPdfEntities(commItem);
                    break;
                }

                case "ENERGIEOVERNAMEDOCUMENT":
                {
                    docEntity = this.GetDocMovingPdfEntities(commItem);
                    break;
                }

                case "TARIEFKAARTOWV2016":
                {
                    docEntity = this.GetContractPdfEmptyEntities();
                    break;
                }

                case "ACTIVATIEMAIL":
                {
                    return null;
                }

                case "LEEG":
                {
                    return null;
                }

                case "DROP_LETTER":
                {
                    docEntity = this.GetContractPdfDropEntities(commItem);
                    break;
                }

                case "WAARSCHUWINGVOORSCHOTNIETMEEROPPAPIER":
                {
                    docEntity = this.GetContractPdfWarningInvoiceNotMoreOnPaperEntities(commItem);
                    break;
                }

                default:
                {
                    throw new NotImplementedException($"The pdf documentttype entity {commItem.DocumentType} is not yet implemented");
                }
            }

            // now get the language
            if (docEntity != null)
            {
                docEntity.LanguageCode = this.GetLanguageCode(commItem);
                docEntity.RegionAbbreviation = this.GetRegionAbbreviation(commItem);
            }

            // now we know the language translate the title
            if (commItem.CommunicationItemId > 0)
            {
                var title = this.TranslateCommItemTitle(commItem, docEntity.LanguageCode);
            }

            return docEntity;
        }

        private string TranslateCommItemTitle(CommunicationItemDocumentType commItem, string languageCode)
        {
            throw new NotImplementedException();
        }

        private string GetRegionAbbreviation(CommunicationItemDocumentType commItem)
        {
            throw new NotImplementedException();
        }

        private string GetLanguageCode(CommunicationItemDocumentType commItem)
        {
            throw new NotImplementedException();
        }

        // VoorschotFactuurDal().GetPdfDataSetEntities
        private DocumentEntity GetInvoicePdfEntities(int? saleLiderId)
        {
            throw new NotImplementedException();
        }

        // VoorschotFactuurDal().GetPdfDataSetEntitiesCreditnota
        private DocumentEntity GetInvoicePdfCreditNoteEntities(int? saleLiderId)
        {
            throw new NotImplementedException();
        }

        // VerbruiksFactuurDal().GetPdfDataSetEntities
        private DocumentEntity GetConsumptionPdfEntities(int? saleLiderId)
        {
            throw new NotImplementedException();
        }

        // VerbruiksFactuurDal().GetPdfDataSetEntitiesCreditnota
        private DocumentEntity GetConsumptionPdfCreditNoteEntities(int? saleLiderId)
        {
            throw new NotImplementedException();
        }

        // ContractDal().GetPdfDataSetEntities
        private DocumentEntity GetContractPdfEntities(int? personId)
        {
            throw new NotImplementedException();
        }

        // ContractDal().GetPdfWelkomBriefDataSetEntities
        private DocumentEntity GetContractPdfWelcomeLetterEntities(int? personId)
        {
            throw new NotImplementedException();
        }

        // DomicilieringDal().GetPdfDataSetEntities
        private DocumentEntity GetDomiciliationPdfEntities(CommunicationItemDocumentType commItem)
        {
            throw new NotImplementedException();
        }

        // AanmaningDal().GetPdfDataSetEntities
        private DocumentEntity GetReminderPdfEntities(CommunicationItemDocumentType commItem)
        {
            throw new NotImplementedException();
        }

        // DetailBerekeningDal().GetPdfDataSetEntities
        private DocumentEntity GetDetailCalculationPdfEntities(CommunicationItemDocumentType commItem)
        {
            throw new NotImplementedException();
        }

        // DocVerhuisDal().GetPdfDataSetEntities
        private DocumentEntity GetDocMovingPdfEntities(CommunicationItemDocumentType commItem)
        {
            throw new NotImplementedException();
        }

        // ContractDal().GetPdfEmptyDataSetEntities
        private DocumentEntity GetContractPdfEmptyEntities()
        {
            throw new NotImplementedException();
        }

        // ContractDal().GetPdfDropDataSetEntities
        private DocumentEntity GetContractPdfDropEntities(CommunicationItemDocumentType commItem)
        {
            throw new NotImplementedException();
        }

        // ContractDal().GetPdfWaarschuwingVoorschotNietMeerOpPapierDataSetEntities
        private DocumentEntity GetContractPdfWarningInvoiceNotMoreOnPaperEntities(CommunicationItemDocumentType commItem)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the HTML for the given templatename from the TemplateFactory
        /// </summary>
        /// <param name="templatename"></param>
        /// <param name="templateFactory"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string GetTemplateHTML(string templateName, TemplateFactory templateFactory, DocumentEntity entity)
        { 
            if (!string.IsNullOrEmpty(templateName))
            {
                var methodInfos = typeof(TemplateFactory).GetMethods();
                var search = templateName;

                foreach (var item in methodInfos)
                {
                    var attributesArray = item.GetCustomAttributes(true);

                    var attr = attributesArray.Where(x =>
                    {
                        if (x.GetType() is MainTemplateNameAttribute)
                        {
                            if ((x as MainTemplateNameAttribute).FileName == search)
                            {
                                return true;
                            }
                        }

                        return false;
                    }).SingleOrDefault();

                    if (attr != null)
                    {
                        try
                        {
                            string html = item.Invoke(templateFactory, new[] { entity }).ToString();
                            this.ResolveSkinFolders(html, entity);
                            this.TranslateLanguagePlaceHolders(html, entity);
                            return html;
                        }
                        catch (Exception ex)
                        {
                            /*GfMessageAndLog.LogDebug(False, False, "GetTemplateHTML", "GetTemplateHTML invoke failed templateFactory [{0}]", Iif(templateFactory Is Nothing, "null", "not null"))
                            GfMessageAndLog.LogDebug(False, False, "GetTemplateHTML", "GetTemplateHTML invoke failed templatename[{0}]", templatename)
                            GfMessageAndLog.LogDebug(False, False, "GetTemplateHTML", ex.Message)
                            if ex.InnerException IsNot Nothing {
                                GfMessageAndLog.LogDebug(False, False, "GetTemplateHTML", ex.InnerException.Message)
                            }*/

                            throw ex;
                        }
                    }
                }
            }

            return string.Empty;
        }

        private void TranslateLanguagePlaceHolders(string html, DocumentEntity entity)
        {
            throw new NotImplementedException();
        }

        private void ResolveSkinFolders(string html, DocumentEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
