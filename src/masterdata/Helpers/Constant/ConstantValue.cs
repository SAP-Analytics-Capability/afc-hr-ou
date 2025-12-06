using System.Collections.Generic;

namespace masterdata.Helpers
{
    public class ConstantValue
    {
        public int constant_id { get; set; }
        public string enelitalia { get; set; }
        public string italiaper { get; set; }
        public string centralperdesc { get; set; }
        public string italiaperdesc { get; set; }
        public string enelproduzione { get; set; }
        public string attrPrevBsMl { get; set; }
        public string gds { get; set; }
        public string sesec { get; set; }
        public string dighub { get; set; }
        public string peo { get; set; }
        public string chapter { get; set; }
        public string digfac { get; set; }
        public string procurement { get; set; }
        public string smd { get; set; }
        public string communications { get; set; }
        public string afc { get; set; }
        public const string businesslinecode = "30";
        public const string staffcode = "32";
        public const string servicecode = "31";
        public string ihol { get; set; }
        public string iser { get; set; }
        public string bhol { get; set; }
        public string bser { get; set; }
        public string gbl { get; set; }
        public string bholBser { get; set; }
        public string gblBholBser { get; set; }
        public string rel { get; set; }
        public string ret { get; set; }
        public string esl { get; set; }
        public string eso { get; set; }
        public string irel { get; set; }
        public string iret { get; set; }
        public string iesl { get; set; }
        public string ieso { get; set; }
        public string ficticious { get; set; }
        public string gco { get; set; }
        public string gat { get; set; }
        public string gbi { get; set; }
        public string gcc { get; set; }
        public string gch { get; set; }
        public string gcr { get; set; }
        public string gcs { get; set; }
        public string gfr { get; set; }
        public string goi { get; set; }
        public string gpq { get; set; }
        public string gco_constants { get; set; }
        public string gco_abbr_constants { get; set; }

        public readonly string ok = "OK";
        public readonly string ko = "KO";

        public const string TypologyGCO = "31-04";
        public const string bREL = "bREL";
        public const string bRET = "bRET";

        public const int codeObjectMultiBusiness = 1;

        public ConstantValue()
        {

        }

        public ConstantValue(string constant)
        {
            this.enelitalia = "Enel Italia";
            this.italiaper = "IT";
            this.centralperdesc = "Central";
            this.italiaperdesc = "Italy";
            this.enelproduzione = "Enel Produzione";
            this.attrPrevBsMl = "MBL";
            this.gds = "GDS";
            this.gco = "GCO";
            this.gat = "GAT";
            this.gbi = "GBI";
            this.gcc = "GCC";
            this.gch = "GCH";
            this.gcr = "GCR";
            this.gcs = "GCS";
            this.gfr = "GFR";
            this.goi = "GOI";
            this.gpq = "GPQ";
            this.gco_constants = "p0(ITH,DHB,CPT,DFC);p1(GPR,PIO);np2(GPR);np3(GAC,GBL,CCS,GHP,GCD,GCT,GTR,GCI,GPM,GCP,ITH)";
            this.gco_abbr_constants = "0503";
            this.sesec = "SSE";
            this.dighub = "DGH";
            this.peo = "HRO";
            this.chapter = "CHP";
            this.digfac = "DGF";
            this.procurement = "PRO";
            this.smd = "SMD";
            this.communications = "CCO";
            this.afc = "AFC";
            this.ihol = "IHOL";
            this.iser = "ISER";
            this.bhol = "BHOL";
            this.bser = "BSER";
            this.gbl = "GBL";
            this.bholBser = "BHOL/BSER";
            this.gblBholBser = "GBL/BHOL/BSER";
            this.rel = "REL";
            this.ret = "RET";
            this.esl = "ESL";
            this.eso = "ESO";
            this.irel = "IREL";
            this.iret = "IRET";
            this.iesl = "IESL";
            this.ieso = "IESO";
            this.ficticious = "fictitious";
            this.ok = "OK";
            this.ko = "KO";
        }
    }
}