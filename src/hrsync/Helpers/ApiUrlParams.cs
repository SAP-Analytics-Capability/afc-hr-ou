namespace hrsync.Helpers
{
    public class ApiUrlParams
    {
        public readonly string ValidityDate = "validity_date=";
        public readonly string ChangedateAttribute ="&changedate_attribute=";
        public readonly string CompanyCode = "&company_code=";
        public readonly string PercCon = "&perc_con=";
        public readonly string NoCostcenter = "&no_costcenter=";
        public readonly string CostcenterDummy = "&costcenter_dummy=";
        public readonly string Gestionali = "&gestionali=";
        public readonly string Limit = "&limit=";
        public readonly string Offset = "&offset=";
        public readonly string Consistenti = "&ou_consistenti=";
        public readonly string Effettivi = "&costcenter_effettivi=";

        public string GetUrlWithParams(string validityDate,
                                        string changedateAttribute,
                                        string companyCode,
                                        string percCon,
                                        string costcenterDummy,
                                        string gestionali,
                                        string limit,
                                        string offset,
                                        string noCostcenter,
                                        string consistenti)
        {
            return (ValidityDate + validityDate
                    + ChangedateAttribute + changedateAttribute
                    + CompanyCode + companyCode
                    + PercCon + percCon
                    + CostcenterDummy + costcenterDummy
                    + Gestionali + gestionali
                    + Limit + limit
                    + Offset + offset
                    + NoCostcenter + noCostcenter
                    + Consistenti + consistenti);
        }

        public string GetUrlWithParams(string validityDate,
                                        string changedateAttribute,
                                        string companyCode,
                                        string percCon,
                                        string costcenterDummy,
                                        string gestionali,
                                        string limit,
                                        string offset,
                                        string noCostcenter,
                                        string consistenti,
                                        string effettivi)
        {
            return (ValidityDate + validityDate
                    + ChangedateAttribute + changedateAttribute
                    + CompanyCode + companyCode
                    + PercCon + percCon
                    + CostcenterDummy + costcenterDummy
                    + Gestionali + gestionali
                    + Limit + limit
                    + Offset + offset
                    + NoCostcenter + noCostcenter
                    + Consistenti + consistenti
                    + Effettivi + effettivi);
        }
    }
}