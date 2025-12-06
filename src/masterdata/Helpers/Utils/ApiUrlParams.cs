namespace masterdata.Helpers
{
    public class ApiUrlParams
    {
        public readonly static string ZVarMacroOrg1 = "param1";
        public readonly static string ZVarMacroOrg2 = "param2";
        public readonly static string ZVarVcs = "param3";
        public readonly static string ValidityDate = "validity_date=";
        public readonly static string ChangedateAttribute = "&changedate_attribute=";
        public readonly static string CompanyCode = "&company_code=";
        public readonly static string PercCon = "&perc_con=";
        public readonly static string NoCostcenter = "&no_costcenter=";
        public readonly static string CostcenterDummy = "&costcenter_dummy=";
        public readonly static string Gestionali = "&gestionali=";
        public readonly static string Limit = "&limit=";
        public readonly static string Offset = "&offset=";
        public readonly static string ChangedDateVal = "&changeddateval=";

        public static string GetURLWithParams(string validityDate,
                                                string changedateAttribute,
                                                string companyCode,
                                                string percCon,
                                                string costcenterDummy,
                                                string gestionali,
                                                string limit,
                                                string offset,
                                                string noCostcenter,
                                                string changedate)
        {
            string urlParams = ValidityDate + validityDate
                                + ChangedateAttribute + changedateAttribute
                                + CompanyCode + companyCode
                                + PercCon + percCon
                                + CostcenterDummy + costcenterDummy
                                + Gestionali + gestionali
                                + Limit + limit
                                + Offset + offset
                                + NoCostcenter + noCostcenter
                                + ChangedDateVal + changedate;
            return urlParams;
        }

        public static string GetURLWithParams(string apiUrl,
                                                string ZVarMacroOrg1,
                                                string ZVarMacroOrg2,
                                                string ZVarVcs)
        {
            string urlParams = string.Empty;
            if (!string.IsNullOrEmpty(ZVarMacroOrg1))
                urlParams = apiUrl.Replace("param1", ZVarMacroOrg1);
            else
                urlParams = apiUrl.Replace("param1", "");

            if (!string.IsNullOrEmpty(ZVarMacroOrg2))
                urlParams = urlParams.Replace("param2", ZVarMacroOrg2);
            else
                urlParams = urlParams.Replace("param2", "");

            if (!string.IsNullOrEmpty(ZVarVcs))
                urlParams = urlParams.Replace("param3", ZVarVcs);
            else
                urlParams = urlParams.Replace("param3", "");

            return urlParams;
        }
    }
}