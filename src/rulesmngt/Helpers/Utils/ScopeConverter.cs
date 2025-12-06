using System;
using System.Text;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Helpers
{
    public class ScopeConverter
    {
        public static List<CompanyRules> ConvertFromScopes(List<CompanyScope> scopelist, out string errormessage)
        {
            errormessage = string.Empty;
            List<CompanyRules> ruleslist = new List<CompanyRules>();

            if (scopelist == null || scopelist.Count == 0)
            {
                return ruleslist;
            }

            try
            {
                foreach (CompanyScope scope in scopelist)
                {
                    CompanyRules rules = new CompanyRules();
                    rules.CompanyCode = 0;
                    rules.NewPrimoCode = scope.AfcNewPrimoCode;
                    rules.SapHrCode = scope.PoSapGlobalCode;
                    rules.SapHrDesc = scope.PoSapGlobalDescr;
                    rules.NewPrimoDesc = scope.AfcNewPrimoDescr;
                    rules.CodeNationSap = scope.PoCountry;
                    rules.E4E = scope.AfcE4e.ToUpper().Equals("NO") ? 0 : 1;
                    rules.AfcCompanyCode = scope.AfcCompanyCode;

                    Area area = new Area();
                    area.AreaId = 0;
                    area.AreaCode = scope.AfcArea;

                    Perimeter perimeter = new Perimeter();
                    perimeter.PerimeterId = 0;
                    perimeter.PerimeterName = scope.AfcPerimeter;
                    perimeter.PerimeterDesc = scope.AfcPerimeterDescr;
                    perimeter.Enabled = 1;

                    Country codenation = new Country();
                    codenation.CountryId = 0;
                    codenation.NationAcronym = scope.AfcCountry;
                    codenation.Nation = scope.AfcCountryDescr;

                    List<BusinessLines> businesslineslist = new List<BusinessLines>();

                    if (!string.IsNullOrEmpty(scope.AfcBlIdis))
                    {
                        BusinessLines bl = new BusinessLines();
                        bl.BusinessLinesId = 0;
                        bl.BusinessLinesName = Constants.BusinessLines[0];
                        bl.Prevalent = 0;
                        bl.Enabled = 1;
                        bl.Desc = string.Empty;
                        businesslineslist.Add(bl);
                    }

                    if (!string.IsNullOrEmpty(scope.AfcBlIgen))
                    {
                        BusinessLines bl = new BusinessLines();
                        bl.BusinessLinesId = 1;
                        bl.BusinessLinesName = Constants.BusinessLines[1];
                        bl.Prevalent = 0;
                        bl.Enabled = 1;
                        bl.Desc = string.Empty;
                        businesslineslist.Add(bl);
                    }

                    if (!string.IsNullOrEmpty(scope.AfcBlItrd))
                    {
                        BusinessLines bl = new BusinessLines();
                        bl.BusinessLinesId = 2;
                        bl.BusinessLinesName = Constants.BusinessLines[2];
                        bl.Prevalent = 0;
                        bl.Enabled = 1;
                        bl.Desc = string.Empty;
                        businesslineslist.Add(bl);
                    }

                    if (!string.IsNullOrEmpty(scope.AfcBlIren))
                    {
                        BusinessLines bl = new BusinessLines();
                        bl.BusinessLinesId = 3;
                        bl.BusinessLinesName = Constants.BusinessLines[3];
                        bl.Prevalent = 0;
                        bl.Enabled = 1;
                        bl.Desc = string.Empty;
                        businesslineslist.Add(bl);
                    }

                    if (!string.IsNullOrEmpty(scope.AfcBlIret))
                    {
                        BusinessLines bl = new BusinessLines();
                        bl.BusinessLinesId = 4;
                        bl.BusinessLinesName = Constants.BusinessLines[4];
                        bl.Prevalent = 0;
                        bl.Enabled = 1;
                        bl.Desc = string.Empty;
                        businesslineslist.Add(bl);
                    }

                    if (!string.IsNullOrEmpty(scope.AfcBlIrel))
                    {
                        BusinessLines bl = new BusinessLines();
                        bl.BusinessLinesId = 5;
                        bl.BusinessLinesName = Constants.BusinessLines[5];
                        bl.Prevalent = 0;
                        bl.Enabled = 1;
                        bl.Desc = string.Empty;
                        businesslineslist.Add(bl);
                    }

                    if (!string.IsNullOrEmpty(scope.AfcBlIser))
                    {
                        BusinessLines bl = new BusinessLines();
                        bl.BusinessLinesId = 6;
                        bl.BusinessLinesName = Constants.BusinessLines[6];
                        bl.Prevalent = 0;
                        bl.Enabled = 1;
                        bl.Desc = string.Empty;
                        businesslineslist.Add(bl);
                    }

                    if (!string.IsNullOrEmpty(scope.AfcBlIhol))
                    {
                        BusinessLines bl = new BusinessLines();
                        bl.BusinessLinesId = 7;
                        bl.BusinessLinesName = Constants.BusinessLines[7];
                        bl.Prevalent = 0;
                        bl.Enabled = 1;
                        bl.Desc = string.Empty;
                        businesslineslist.Add(bl);
                    }

                    if (!string.IsNullOrEmpty(scope.AfcBlInuk))
                    {
                        BusinessLines bl = new BusinessLines();
                        bl.BusinessLinesId = 8;
                        bl.BusinessLinesName = Constants.BusinessLines[8];
                        bl.Prevalent = 0;
                        bl.Enabled = 1;
                        bl.Desc = string.Empty;
                        businesslineslist.Add(bl);
                    }

                    if (!string.IsNullOrEmpty(scope.AfcBlIeso))
                    {
                        BusinessLines bl = new BusinessLines();
                        bl.BusinessLinesId = 9;
                        bl.BusinessLinesName = Constants.BusinessLines[9];
                        bl.Prevalent = 0;
                        bl.Enabled = 1;
                        bl.Desc = string.Empty;
                        businesslineslist.Add(bl);
                    }

                    if (!string.IsNullOrEmpty(scope.AfcBlIesl))
                    {
                        BusinessLines bl = new BusinessLines();
                        bl.BusinessLinesId = 10;
                        bl.BusinessLinesName = Constants.BusinessLines[10];
                        bl.Prevalent = 0;
                        bl.Enabled = 1;
                        bl.Desc = string.Empty;
                        businesslineslist.Add(bl);
                    }
                  
                  
                    if (!string.IsNullOrEmpty(scope.AfcBlPrevalent))
                    {
                        BusinessLines bl = new BusinessLines();
                        bl.BusinessLinesId = 11;
                        bl.BusinessLinesName = scope.AfcBlPrevalent;
                        bl.Prevalent = 1;
                        bl.Enabled = 1;
                        bl.Desc = string.Empty;
                        businesslineslist.Add(bl);
                    }

                    if (!string.IsNullOrEmpty(scope.AfcBlImob))
                    {
                        BusinessLines bl = new BusinessLines();
                        bl.BusinessLinesId = 12;
                        bl.BusinessLinesName = Constants.BusinessLines[11];
                        bl.Prevalent = 0;
                        bl.Enabled = 1;
                        bl.Desc = string.Empty;
                        businesslineslist.Add(bl);
                    }

                   

                    rules.Area = area;
                    rules.Perimeter = perimeter;
                    rules.CodeNation = codenation;
                    rules.BusinessLines = businesslineslist;

                    rules.FlagConsolidation = scope.FlagConsolidation;
                    
                    ruleslist.Add(rules);
                }
            }
            catch (Exception ex)
            {
                ruleslist = null;
                errormessage = ex.ToString();
            }
            return ruleslist;
        }
    }
}
