using System;

namespace bwsync.Helpers
{
    public class ApiFilteringChecker
    {
        public static void BWApiFilterChecker(string ZVarMacroOrg1, string ZVarMacroOrg2, string ZVarVcs)
        {
            try
            {
                if (string.IsNullOrEmpty(ZVarMacroOrg1))
                {
                    throw new System.ArgumentException("Macro Org 1 filter cannot be null!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            try
            {
                if (string.IsNullOrEmpty(ZVarMacroOrg2))
                {
                    throw new System.ArgumentException("Macro Org 2 filter cannot be null!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}