using System;

namespace GreenFever.Invoice.Dal.Dto.Enums
{
    /// <summary>
    /// Documenttpes with code and corresponding seqdocumenttype
    /// TODO : BART ANRIJS : 20/08/2015
    /// THIS ENUM IS DEPRECATED AND IS ONLY KEPT FOR BACKWARD COMPATIBILITY REASON, IN ORDER TO MINIMIZE IMPACT ON PERIFERAL APPS AND SERVICES
    /// IT SHOULD BE REPLACED BY 'DocumentTypeCode' ASAP 
    /// Iaroslav Iefimenko 23/11/2018: This enum is used for PdfGeneration and cannot be translated because it contains names from database
    /// </summary>
    /// <remarks></remarks>
    [Obsolete("Replace by DocumentTypeCode. eg: Dim docTypeCode AS DocumentTypeCode = DocumentTypeDal.GetCode(WELKOMSTMAIL)")]
    public enum DocumentTypeCodes
    {
        AANMANING1 = 7,
        AANMANING2 = 8,
        ACTIVATIEMAIL = 10,
        ACTIVATIEMAIL_NIEUW_EMAIL = 16,
        ACTIVATIEMAIL_NIEUW_REKENINGNUMMER = 17,
        FACTUUR = 0,
        ARCHIEF = 5,
        BIJKOMENDE_AANVRAAG = 15,
        BRIEF = 2,
        CONTRACT = 21,
        DOMICILIERINGSAANVRAAG = 3,
        HANDMATIG = 19,
        INGEBREKESTELLING = 9,
        LEEG = 4,
        PRIJSCALCULATOR = 18,
        SLOTFACTUUR = 6,
        VERHUISMAIL_NIEUWE_BEWONER = 13,
        VERHUISMAIL_OUDE_BEWONER = 14,
        VOORSCHOTFACTUUR = 1,
        WACHTEN_EAN_NUMMERS = 12,
        WACHTWOORD_VERGETEN = 20,
        WELKOMSTMAIL = 11,
        WIJZIG_GEGEVENS = 22,
        NIEUW_CONTRACT_MAIL = 23,
        MAIL_NIEUW_WACHTWOORD = 24,
        VOORSCHOTFACTUUR_CREDIT = 25,
        CREDITNOTA = 26,
        DROP_KLANT = 27,
        DROP_FOUTIEF = 28,
        AFBETALINGSPLAN = 29,
        ENERGIEOVERNAMEDOCUMENT = 30,
        VERHUISONVOLLEDIG = 31,
        VERHUISVERWITTIGING = 36,
        VERHUISOVERNEMER_MAIL = 37,
        VERHUISOVERNEMER_EMAIL_ONGEKEND = 38,
        DOMICILIERINGSHERINNERING = 32,
        INGEBREKEONDANKSAP = 33,
        AFBETALINGSPLANNIETGEVOLGD = 34,
        VERWITTIGING = 35,
        START_LEVERING = 41,
        EINDE_CONTRACT = 42,
        VOORSCHOT_AANGEPAST_MAIL = 43,
        INJECTIEFACTUUR = 44,
        INJECTIESLOTFACTUUR = 45,
        DECENTRALEPRODUCTIEWELKOM = 46,

        // EODREMINDER = 47
        ACTIVATIEREMINDER = 48,
        WELKOMBRIEF = 57,
        TARIEFKAARTOWV2016 = 58,
        VERWITTIGINGNALAATSTE = 59,
        MAILNIETVERGETENBETALEN = 60,
        MAILNIETVERGETENBETALENMETDOM = 61,
        MAILDEBITERING = 62,
        DROP_LETTER = 63,
        WAARSCHUWINGVOORSCHOTNIETMEEROPPAPIER = 64,
    }
}
