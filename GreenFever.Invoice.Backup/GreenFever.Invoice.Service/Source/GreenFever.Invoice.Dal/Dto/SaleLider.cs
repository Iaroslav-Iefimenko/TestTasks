using System;

namespace GreenFever.Invoice.Dal.Dto
{
    // verkoopkop
    public class SaleLider
    {
        // "SEQVERKOOPKOP" NUMBER(15,0) NOT NULL
        public int Id { get; set; }

        // "CREUSR" VARCHAR2(32 BYTE) DEFAULT 'USER'
        public string CreateUser { get; set; }

        // "UPDUSR" VARCHAR2(32 BYTE) DEFAULT 'USER'
        public string UpdateUser { get; set; }

        // "CREDAT" DATE DEFAULT SYSDATE
        public DateTime CreateDate { get; set; }

        // "UPDDAT" DATE DEFAULT SYSDATE
        public DateTime UpdateDate { get; set; }

        // "DOCUMENTNR" VARCHAR2(20 BYTE)
        public string DocumentNumber { get; set; }

        // "SEQKLANT" NUMBER(15,0) REFERENCES "GREENFEVER"."PERSOON" ("SEQPERSOON")
        public int ClientId { get; set; }

        // "KLANTNAAM" VARCHAR2(50 BYTE)
        public string ClientName { get; set; }

        // "KLANTADRES" VARCHAR2(50 BYTE)
        public string ClientAddress { get; set; }

        // "KLANTADRES2" VARCHAR2(50 BYTE)
        public string ClientAddress2 { get; set; }

        // "KLANTGEMEENTE" VARCHAR2(50 BYTE)
        public string ClientMunicipality { get; set; }

        // "KLANTPOSTCODE" VARCHAR2(20 BYTE)
        public string ClientPostCode { get; set; }

        // "KLANTLAND" VARCHAR2(20 BYTE)
        public string ClientLand { get; set; }

        // "DOCUMENTDATUM" DATE
        public DateTime DocumentDate { get; set; }

        // "BOEKINGSDATUM" DATE
        public DateTime BookingDate { get; set; }

        // "VERVALDATUM" DATE
        public DateTime ExpiredDate { get; set; }

        // "BETALINGSTERMIJN" VARCHAR2(10 BYTE)
        public string TermOfPayment { get; set; }

        // "SEQVERKOOPSDOCUMENTTYPE" NUMBER(15,0) REFERENCES "GREENFEVER"."VERKOOPSDOCUMENTTYPE"
        public int SaleDocumentTypeId { get; set; }

        // "KLANTNUMMER" VARCHAR2(11 BYTE)
        public string ClientNumber { get; set; }

        // "BTW_NUMMER" VARCHAR2(20 BYTE)
        public string VatNumber { get; set; }

        // "CONTACTPERSOON" VARCHAR2(200 BYTE)
        public string ContactPerson { get; set; }

        // "VOORSCHOTFACTUUR" NUMBER(1,0) DEFAULT 0
        public bool Invoice { get; set; }

        // "VOORSCHOTINFORMATIE" VARCHAR2(200 BYTE)
        public string InvoiceInformation { get; set; }

        // "SEQFACTURATIERUN" NUMBER(15,0) REFERENCES "GREENFEVER"."FACTURATIERUN"
        public int InvoiceProcessingRunId { get; set; }

        // "REKENINGNUMMER" VARCHAR2(25 BYTE)
        public string AccountNumber { get; set; }

        // "DOMICILIERINGSNUMMER" VARCHAR2(25 BYTE), 
        public string DwellingRingNumber { get; set; }

        // "SEQBETALINGSWIJZE" NUMBER(15,0)
        public int PaymentMethodId { get; set; }

        // "SEQVERKOOPKOP_PARENT" NUMBER(15,0)
        public int ParentId { get; set; }

        // "SEQNAVISIONSTATUS" NUMBER(15,0) DEFAULT 0 REFERENCES "GREENFEVER"."NAVISIONSTATUS"
        public int NavisionStatusId { get; set; }
    
        // "VEREFFENINGSSOORT" VARCHAR2(15 BYTE)
        public string ClearingType { get; set; }

        // "VEREFFENINGSNUMMER" VARCHAR2(15 BYTE)
        public string ClearingNumber { get; set; }

        // "VOORLOPIG_DOCUMENTNR" VARCHAR2(20 BYTE)
        public string ProvisionalDocumentNumber { get; set; }
    
        // "GA_AFREKENDATUM" DATE
        public DateTime GaCheckoutDate { get; set; }

        // "GA_BEGIN_PERIODE" DATE
        public DateTime GaBeginPeriod { get; set; }

        // "GA_SEQPERIODE" NUMBER(15,0) REFERENCES "GREENFEVER"."PERIODE"
        public int GaPeriodId { get; set; }

        // "GA_AANTAL_VOORSCHOTTEN" NUMBER(8,0)
        public int GaInvoicesAmount { get; set; }

        // "GA_TELLER_VOORSCHOTTEN" NUMBER(8,0)
        public int GaInvoicesCounter { get; set; }

        // "GA_VOLGENDE_DATUM" DATE
        public DateTime GaFollowingDate { get; set; }

        // "SEQGELIJKTIJDIGE_AFREK" NUMBER(15,0) REFERENCES "GREENFEVER"."GELIJKTIJDIGE_AFREK"
        public int SimultaneousDepartureId { get; set; }

        // "PRINTCHAR" VARCHAR2(1 BYTE) DEFAULT 'A', 
        public string PrintChar { get; set; }

        // "DOMICILIERINGSOMS" VARCHAR2(56 BYTE) 
        public string DwellingRingOms { get; set; }

        // "ONDERNEMINGSNUMMER" VARCHAR2(50 BYTE) 
        public string CompanyNumber { get; set; }
        
        // "GENERATIEDATUM" DATE
        public DateTime GenerationDate { get; set; }

        // "SLOTFACTUUR" NUMBER(1,0)
        public int SlotInvoice { get; set; }

        // "OPNIEUW" NUMBER(1,0), 
        public bool OpNew { get; set; }
    }
}