using System;

namespace hrsync.Helpers
{
    public class ApiFilteringChecker
    {
        public static void HRApiFilterChecker(string validityDate, string changedateAttribute,
                                        string companyCode, string percCon,
                                        string costcenterDummy, string gestionali,
                                        string limit, string offset, string noCostcenter)
        {
            try
            {
                if (!string.IsNullOrEmpty(validityDate))
                {
                    string date = validityDate.Replace("-", "/");
                    DateTime filter = DateTime.MinValue;

                    if (!DateTime.TryParse(date, out filter))
                    {
                        throw new Exception("Not a validity date!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }

            try
            {
                if (!string.IsNullOrEmpty(changedateAttribute))
                {
                    string date = changedateAttribute.Replace("-", "/");
                    DateTime filter = DateTime.MinValue;

                    if (!DateTime.TryParse(date, out filter))
                    {
                        throw new Exception("Not a changed attribute date!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }

            try
            {
                if (!string.IsNullOrEmpty(companyCode))
                {
                    int filter = 0;
                    if (!int.TryParse(companyCode, out filter) || companyCode.Length > 4)
                    {
                        throw new Exception("Not a valid company code");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }

            try
            {
                if (!string.IsNullOrEmpty(percCon))
                {
                    int filter = 0;
                    if (!int.TryParse(percCon, out filter))
                    {
                        throw new Exception("Not a valid percentage format!");
                    }
                    else if (filter < 0 || filter > 100)
                    {
                        throw new Exception("Not a valid percentage!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }

            try
            {
                if (!string.IsNullOrEmpty(noCostcenter))
                {
                    if (!noCostcenter.Equals("Y", StringComparison.OrdinalIgnoreCase) &&
                        !noCostcenter.Equals("N", StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception("Invalid value for no Cost Center filter");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }

            try
            {
                if (!string.IsNullOrEmpty(costcenterDummy))
                {
                    if (!costcenterDummy.Equals("Y", StringComparison.OrdinalIgnoreCase) &&
                        !costcenterDummy.Equals("N", StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception("Invalid value for no Dummy Cost Center filter");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }

            try
            {
                if (!string.IsNullOrEmpty(gestionali))
                {
                    if (!gestionali.Equals("Y", StringComparison.OrdinalIgnoreCase) &&
                        !gestionali.Equals("N", StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception("Invalid value for filter gestionali");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }

            try
            {
                if (!string.IsNullOrEmpty(limit))
                {
                    int filter = 0;
                    if (!int.TryParse(limit, out filter))
                    {
                        throw new Exception("Limit filter is not a number!");
                    }
                    else if (filter <= 0)
                    {
                        throw new Exception("Limit is invalid: cannot apply limit on a negative number");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }

            try
            {
                if (!String.IsNullOrEmpty(offset))
                {
                    int filter = 0;
                    if (!int.TryParse(offset, out filter))
                    {
                        throw new Exception("Offset filter is not a number!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(ex.Message);
            }
        }
    }
}