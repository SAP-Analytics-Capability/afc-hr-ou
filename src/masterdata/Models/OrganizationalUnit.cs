using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace masterdata.Models
{
    public class OrganizationalUnit
    {
        [JsonProperty(PropertyName = "ou_code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "ou_description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "ou_status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "ou_status_update_flag")]
        public string StatusUpdateFlag { get; set; }

        [JsonProperty(PropertyName = "ou_company_country")]
        public string CompanyCountry { get; set; }

        [JsonProperty(PropertyName = "ou_start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "ou_end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty(PropertyName = "company")]
        public string Company { get; set;}

        [JsonProperty(PropertyName = "company_description")]
        public string CompanyDesccription { get; set;}

        [JsonProperty(PropertyName = "prevalent_type")]
        public string PrevalentType { get; set;}
        
        [JsonProperty(PropertyName = "prevalent_type_description")]
        public string PrevalentTypeDesc { get; set;}

        [JsonProperty(PropertyName = "prevalent_code")]
        public int PrevalentCode { get; set;}

        [JsonProperty(PropertyName = "perimeter_code")]
        public int PerimeterCode { get; set;}

        [JsonProperty(PropertyName = "perimeter_description")]
        public string PerimeterDescription { get; set;}
        
        [JsonProperty(PropertyName="npa_bl_code1")]
        public string NpaBlCode1 {get; set;}

        [JsonProperty(PropertyName="npa_bl_desc1")]
        public string NpaDesc1{get; set;}

        [JsonProperty(PropertyName="npa_bl_code2")]
        public string NpaBlCode2 {get; set;}

        [JsonProperty(PropertyName="npa_bl_desc2")]
        public string NpaDesc2{get; set;}

        [JsonProperty(PropertyName="npa_bl_code3")]
        public string NpaBlCode3 {get; set;}

        [JsonProperty(PropertyName="npa_bl_desc3")]
        public string NpaDesc3 {get; set;}

        [JsonProperty(PropertyName="npa_bl_code4")]
        public string NpaBlCode4 {get; set;}

        [JsonProperty(PropertyName="npa_bl_desc4")]
        public string NpaDesc4 {get; set;}

        [JsonProperty(PropertyName="npa_bl_code5")]
        public string NpaBlCode5 {get; set;}

        [JsonProperty(PropertyName="npa_bl_desc5")]
        public string NpaDesc5 {get; set;}

        [JsonProperty(PropertyName="npa_service_code1")]
        public string NpaServiceCode1 {get; set;}

        [JsonProperty(PropertyName="npa_service_desc1")]
        public string NpaServiceDesc1 {get; set;}

        [JsonProperty(PropertyName="npa_service_code2")]
        public string NpaServiceCode2 {get; set;}

        [JsonProperty(PropertyName="npa_service_desc2")]
        public string NpaServiceDesc2 {get; set;}

        [JsonProperty(PropertyName="npa_service_code3")]
        public string NpaServiceCode3 {get; set;}

        [JsonProperty(PropertyName="npa_service_desc3")]
        public string NpaServiceDesc3 {get; set;}

        [JsonProperty(PropertyName="npa_service_code4")]
        public string NpaServiceCode4 {get; set;}

        [JsonProperty(PropertyName="npa_service_desc4")]
        public string NpaServiceDesc4 {get; set;}

        [JsonProperty(PropertyName="npaservice_code5")]
        public string NpaServiceCode5 {get; set;}

        [JsonProperty(PropertyName="npa_service_desc5")]
        public string NpaServiceDesc5 {get; set;}

        [JsonProperty(PropertyName="npa_staff_code1")]
        public string NpaStaffCode1 {get; set;}

        [JsonProperty(PropertyName="npa_staff_desc1")]
        public string NpaStaffDesc1 {get; set;}

        [JsonProperty(PropertyName="npa_staff_code2")]
        public string NpaStaffCode2 {get; set;}

        [JsonProperty(PropertyName="npa_staff_desc2")]
        public string NpaStaffDesc2 {get; set;}

        [JsonProperty(PropertyName="npa_staff_code3")]
        public string NpaStaffCode3 {get; set;}

        [JsonProperty(PropertyName="npa_staff_desc3")]
        public string NpaStaffesc3 {get; set;}

        [JsonProperty(PropertyName="npa_staff_code4")]
        public string NpaStaffCode4 {get; set;}

        [JsonProperty(PropertyName="npa_staff_desc4")]
        public string NpaStaffDesc4 {get; set;}

        [JsonProperty(PropertyName="npa_staff_code5")]
        public string NpaStaffCode5 {get; set;}

        [JsonProperty(PropertyName="npa_staff_desc5")]
        public string NpaStaffDesc5 {get; set;}
        

        public OrganizationalUnit()
        {
            this.Code = "";
            this.Description = "description";
            this.Status = "status";
            this.StatusUpdateFlag = "statusUpdate";
            this.CompanyCountry = "CompanyCountry";
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;
            this.Company = "company";
            this.CompanyDesccription = "companyDescription";
            this.PrevalentType = "prevalentType";
            this.PrevalentTypeDesc = "prevalentTypeDesc";
            this.PrevalentCode = 0;
            this.PerimeterDescription = "PerimeterDescription";
        }
    }
}