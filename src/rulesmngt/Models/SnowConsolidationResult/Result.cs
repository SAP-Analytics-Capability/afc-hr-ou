using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace rulesmngt.Models.SnowConsolidationResult
{
  public class Result
{
    public string u_afc_iren { get; set; }
    public string u_afc_area { get; set; }
    public string u_afc_irel { get; set; }
    public string u_afc_prevalent { get; set; }
    public string u_afc_perimeter_descr { get; set; }
    public string u_info_rpa { get; set; }
    public string u_po_bpc_descr { get; set; }
    public string sys_updated_on { get; set; }
    public string u_afc_consolidation_perc { get; set; }
    public string u_po_perimeter_bpc { get; set; }
    public string u_afc_igen { get; set; }
    public string u_afc_exit_date { get; set; }
    public string u_afc_idis { get; set; }
    public string sys_id { get; set; }
    public string sys_updated_by { get; set; }
    public string sys_created_on { get; set; }
    public string u_afc_ihol { get; set; }
    public string u_sap_hr_global_code { get; set; }
    public string u_afc_inuk { get; set; }
    public string u_afc_entry_date { get; set; }
    public string u_afc_ieso { get; set; }
    public string sys_created_by { get; set; }
    public string u_afc_cdc_dummy { get; set; }
    public string u_afc_comp_currency { get; set; }
    public string u_afc_iesl { get; set; }
    public string u_afc_imob { get; set; }
    public string u_newprimo_code { get; set; }
    public string u_po_country { get; set; }
    public string sys_mod_count { get; set; }
    public string u_po_country_description { get; set; }
    public string u_po_sap_hr_company_template { get; set; }
    public string sys_tags { get; set; }
    public string u_sap_hr_global_descr { get; set; }
    public string u_afc_perimeter { get; set; }
    public string u_po_bpc_code { get; set; }
    public string u_afc_newprimo_code { get; set; }
    public string u_flag_consolidation { get; set; }
    public string u_afc_itrd { get; set; }
    public string u_po_driver { get; set; }
    public string u_afc_company_code { get; set; }
    public string u_descr_extended { get; set; }
    public string u_afc_country { get; set; }
    public string u_afc_iret { get; set; }
    public string u_afc_company_e4e { get; set; }
    public string u_afc_iser { get; set; }
    public string u_afc_country_descr { get; set; }
    public string u_review_hld_approver_status { get; set; }
        
    }

    public class RootObjectConsolidation
    {
        public List<Result> result { get; set; }
    }

    public class ResultList
    {
        public List<Result> results { get; set; }
    }
}