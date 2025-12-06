using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using rulesmngt.Models.Configuration;
using rulesmngt.Models.SnowConsolidationResult;
using rulesmngt.Models.SnowTranscodingResult;

namespace rulesmngt.Helpers.Utils
{
    public class CatalogUtils : ICatalogUtils
    {
   
        private ILogger Logger;
        private readonly IOptions<DatabaseConfiguration> DataSource;
        public CatalogUtils(IOptions<DatabaseConfiguration> datasource, ILoggerFactory loggerFactory)
        {
            this.DataSource = datasource;
            this.Logger = loggerFactory.CreateLogger<CatalogUtils>();
        }

        public CatalogUtils()
        {
        }
        public List<CompanyScope> ConvertToAfcConsolidateCatalog(RootObjectConsolidation snowCatalog)
        {
            List<CompanyScope> companies = new List<CompanyScope>();

            foreach (Models.SnowConsolidationResult.Result snowResult in snowCatalog.result)
            {
                CompanyScope company = new CompanyScope();

                company.AfcNewPrimoCode = snowResult.u_afc_newprimo_code;
                company.AfcNewPrimoDescr = snowResult.u_descr_extended;
                company.AfcBlIdis = snowResult.u_afc_idis;
                company.AfcBlIgen = snowResult.u_afc_igen;
                company.AfcBlItrd = snowResult.u_afc_itrd;
                company.AfcBlIren = snowResult.u_afc_iren;
                company.AfcBlIret = snowResult.u_afc_iret;
                company.AfcBlIrel = snowResult.u_afc_irel;
                company.AfcBlIser = snowResult.u_afc_iser;
                company.AfcBlIhol = snowResult.u_afc_ihol;
                company.AfcBlInuk = snowResult.u_afc_inuk;
                company.AfcBlIeso = snowResult.u_afc_ieso;
                company.AfcBlIesl = snowResult.u_afc_iesl;
                company.AfcBlImob = snowResult.u_afc_imob;
                company.AfcBlPrevalent = snowResult.u_afc_prevalent;
                company.AfcArea = snowResult.u_afc_area;
                company.AfcPerimeter = snowResult.u_afc_perimeter;
                company.AfcPerimeterDescr = snowResult.u_afc_perimeter_descr;
                company.AfcCountry = snowResult.u_afc_country;
                company.AfcCountryDescr = snowResult.u_afc_country_descr;
                company.AfcE4e = snowResult.u_afc_company_e4e;
                company.PoSapGlobalCode = snowResult.u_sap_hr_global_code;
                company.PoSapGlobalDescr = snowResult.u_sap_hr_global_descr;
                company.PoNewPrimoCode = snowResult.u_newprimo_code;
                company.PoCountry = snowResult.u_po_country;
                company.PoCountryDescr = snowResult.u_po_country_description;
                company.AfcCompanyCode = snowResult.u_afc_company_code;
                company.CdcDummy = snowResult.u_afc_cdc_dummy;
                company.FlagConsolidation = snowResult.u_flag_consolidation;

                companies.Add(company);
            }

            return companies;
        }

        public List<ActivityAssociation> ConvertToAfcActivityCatalog(RootObjectTranscoding snowCatalogTranscoding)
        {
            List<ActivityAssociation> activities = new List<ActivityAssociation>();

            foreach (Models.SnowTranscodingResult.Result snowResult in snowCatalogTranscoding.result)
            {
                ActivityAssociation activity = new ActivityAssociation();
                activity.PoAttribute = snowResult.u_attribute;
                if (!string.IsNullOrEmpty(snowResult.u_object_type))
                {
                    activity.ObjectTypeId = Convert.ToInt32(snowResult.u_object_type).ToString();
                }
                if (!string.IsNullOrEmpty(snowResult.u_object_id))
                {
                    activity.PoObjectId = Convert.ToInt32(snowResult.u_object_id).ToString();
                }
                if (!string.IsNullOrEmpty(snowResult.u_object_abbr))
                {
                    activity.PoObjectAbbr = Convert.ToInt32(snowResult.u_object_abbr).ToString();
                }

                activity.PoBpcCode = snowResult.u_bpc_code;
                activity.PoDescription = snowResult.u_description;
                activity.PoNpa = snowResult.u_not_prevalent_activity;

                if (snowResult.u_perimeter.Contains('='))
                {
                    activity.PoPerimeter = "1";
                }
                else if (snowResult.u_perimeter.Contains("<>"))
                {
                    activity.PoPerimeter = "0";
                }
                else
                {
                    activity.PoPerimeter = snowResult.u_perimeter;
                }

                activity.AfcMacroorg1 = snowResult.u_afc_macroorg1;
                activity.AfcMacroorg1Desc = snowResult.u_afc_macroorg1_descr;
                activity.AfcMacroorg2 = snowResult.u_afc_macroorg2;
                activity.AfcMacroorg2Desc = snowResult.u_afc_macroorg2_descr;
                activity.OrProcess = snowResult.u_afc_process;
                activity.OrProcessDesc = snowResult.u_afc_process_description;
                activity.OrOrganization = snowResult.u_afc_organization;
                activity.OrOrganizationDesc = snowResult.u_afc_organization_descr;
                activity.OrVcs = snowResult.u_afc_vcs;
                activity.OrVcsDesc = snowResult.u_afc_vcs_descr;
                if (!string.IsNullOrEmpty(snowResult.u_object_id))
                {
                    activity.BpcObjectId = Convert.ToInt32(snowResult.u_object_id).ToString();
                }
                if (!string.IsNullOrEmpty(snowResult.u_object_abbr))
                {
                    activity.BpcObjectAbbr = Convert.ToInt32(snowResult.u_object_abbr).ToString();
                }

                activity.BpcCode = snowResult.u_bpc_code;

                if (!string.IsNullOrEmpty(snowResult.u_object_abbr))
                {
                    activity.BpcDesc = Convert.ToInt32(snowResult.u_object_abbr).ToString();
                }

                activity.TypologyObject = snowResult.u_po_father_code;

                activities.Add(activity);
            }

            return activities;

        }

        public bool ElaborationDBCleaningTranscoding()
        {
            Logger.LogInformation("Elaboration DB Cleaning started: " + DateTime.Now);
            bool isClean = false;
            int num = 0;

            try
            {
                using (RulesContext context = new RulesContext(DataSource))
                {
                    num = context.Database.ExecuteSqlCommand("select afchr_int_schema.cleaningtranscoding_function()");
                }

                if (num != 0)
                    isClean = true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("ERROR during DB Cleaning execution", DateTime.Now));
                return false;
            }

            Logger.LogInformation("Elaboration DB Cleaning finished:" + DateTime.Now);
            return isClean;
        }

        public bool ElaborationDBCleaningConsolidating()
        {
            Logger.LogInformation("Elaboration DB Cleaning started: " + DateTime.Now);
            bool isClean = false;
            int num = 0;

            try
            {
                using (RulesContext context = new RulesContext(DataSource))
                {
                    num = context.Database.ExecuteSqlCommand("select afchr_int_schema.cleaningConsolidate_function()");
                }

                if (num != 0)
                    isClean = true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("ERROR during DB Cleaning execution", DateTime.Now));
                return false;
            }

            Logger.LogInformation("Elaboration DB Cleaning finished:" + DateTime.Now);
            return isClean;
        }

        public bool AddTranscoding(List<ActivityAssociation> activities)
        {
            try
            {
                using (RulesContext context = new RulesContext(DataSource))
                {
                    foreach (ActivityAssociation activity in activities)
                    {
                        EntityEntry<ActivityAssociation> ee = context.Add(activity);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to add new ActivityAssociation");
            }
            return true;
        }

        public bool AddConsolidated(List<CompanyScope> companies)
        {

            try
            {
                using (RulesContext context = new RulesContext(DataSource))
                {
                    foreach (CompanyScope company in companies)
                    {
                        EntityEntry<CompanyScope> ee = context.Add(company);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unable to add new ActivityAssociation");
            }
            return true;
        }

        public bool isEmptyTranscoding()
        {
            List<ActivityAssociation> associationlist = null;

            try
            {
                using (RulesContext context = new RulesContext(DataSource))
                {
                    associationlist = (from al in context.ActivityAssociation select al).ToList<ActivityAssociation>();
                }
                if (associationlist.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get the full list of the activity associations.", DateTime.Now));
                return false;
            }
        }

        public bool isEmptyConsolidate()
        {
            List<CompanyScope> companies = null;

            try
            {
                using (RulesContext context = new RulesContext(DataSource))
                {
                    companies = context.CompanyScope.ToList<CompanyScope>();
                }

                if (companies.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("{0} - Unable to get company scope", DateTime.Now));
                return false;
            }
        }

        public bool RollbackTranscoding()
        {
            int num = 0;
            bool isClean = false;
            try
            {
                using (RulesContext context = new RulesContext(DataSource))
                {
                    num = context.Database.ExecuteSqlCommand("select afchr_int_schema.rollbackConsolidate_function()");
                }

                if (num != 0)
                    isClean = true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("ERROR during DB Cleaning execution", DateTime.Now));
                return false;
            }

            return isClean;
        }

        public bool RollbackConsolidated()
        {
            int num = 0;
            bool isClean = false;
            try
            {
                using (RulesContext context = new RulesContext(DataSource))
                {
                    num = context.Database.ExecuteSqlCommand("select afchr_int_schema.rollbackConsolidate_function()");
                }

                if (num != 0)
                    isClean = true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, string.Format("ERROR during DB Cleaning execution", DateTime.Now));
                return false;
            }

            return isClean;
        }
    }
}