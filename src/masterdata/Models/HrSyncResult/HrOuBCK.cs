using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace masterdata.Models.HrSyncResult
{
    public class HrOuBCK
    {
     [Key]
        [JsonProperty(PropertyName = "organization_unit_id", NullValueHandling = NullValueHandling.Ignore)]
        public int hrmdou_id { get; set; }

        [JsonProperty(PropertyName = "typology_association")]
        public string typologyAssociation { get; set; }

        [JsonProperty(PropertyName = "u_org")]
        public string UOrg { get; set; }

        [JsonProperty(PropertyName = "u_org_desc")]
        public string UOrgDesc { get; set; }

        [JsonProperty(PropertyName = "start_date")]
        public string StartDate { get; set; }

        [JsonProperty(PropertyName = "end_date")]
        public string EndDate { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }

        [JsonProperty(PropertyName = "company_desc")]
        public string CompanyDesc { get; set; }

        [JsonProperty(PropertyName = "cost_center")]
        public string CostCenter { get; set; }

        [JsonProperty(PropertyName = "pred_attr")]
        public string PredAttr { get; set; }

        [JsonProperty(PropertyName = "pred_attr_desc")]
        public string PredAttrDesc { get; set; }

        [JsonProperty(PropertyName = "attr_prev_cod")]
        public string AttrPrevCod { get; set; }

        [JsonProperty(PropertyName = "attr_prev_desc")]
        public string AttrPrevDesc { get; set; }

        [JsonProperty(PropertyName = "perimeter")]
        public string Perimeter { get; set; }

        [JsonProperty(PropertyName = "perimeter_desc")]
        public string PerimeterDesc { get; set; }

        [JsonProperty(PropertyName = "bus_line_cod_1")]
        public string BusLineCod1 { get; set; }

        [JsonProperty(PropertyName = "bus_line_desc_1")]
        public string BusLineDesc1 { get; set; }

        [JsonProperty(PropertyName = "bus_line_cod_2")]
        public string BusLineCod2 { get; set; }

        [JsonProperty(PropertyName = "bus_line_desc_2")]
        public string BusLineDesc2 { get; set; }

        [JsonProperty(PropertyName = "bus_line_cod_3")]
        public string BusLineCod3 { get; set; }

        [JsonProperty(PropertyName = "bus_line_desc_3")]
        public string BusLineDesc3 { get; set; }

        [JsonProperty(PropertyName = "bus_line_cod_4")]
        public string BusLineCod4 { get; set; }

        [JsonProperty(PropertyName = "bus_line_desc_4")]
        public string BusLineDesc4 { get; set; }

        [JsonProperty(PropertyName = "bus_line_cod_5")]
        public string BusLineCod5 { get; set; }

        [JsonProperty(PropertyName = "bus_line_desc_5")]
        public string BusLineDesc5 { get; set; }

        [JsonProperty(PropertyName = "serv_cod_1")]
        public string ServCod1 { get; set; }

        [JsonProperty(PropertyName = "serv_desc_1")]
        public string ServDesc1 { get; set; }

        [JsonProperty(PropertyName = "serv_cod_2")]
        public string ServCod2 { get; set; }

        [JsonProperty(PropertyName = "serv_desc_2")]
        public string ServDesc2 { get; set; }

        [JsonProperty(PropertyName = "serv_cod_3")]
        public string ServCod3 { get; set; }

        [JsonProperty(PropertyName = "serv_desc_3")]
        public string ServDesc3 { get; set; }

        [JsonProperty(PropertyName = "serv_cod_4")]
        public string ServCod4 { get; set; }

        [JsonProperty(PropertyName = "serv_desc_4")]
        public string ServDesc4 { get; set; }

        [JsonProperty(PropertyName = "serv_cod_5")]
        public string ServCod5 { get; set; }

        [JsonProperty(PropertyName = "serv_desc_5")]
        public string ServDesc5 { get; set; }

        [JsonProperty(PropertyName = "staff_cod_1")]
        public string StaffCod1 { get; set; }

        [JsonProperty(PropertyName = "staff_desc_1")]
        public string StaffDesc1 { get; set; }

        [JsonProperty(PropertyName = "staff_cod_2")]
        public string StaffCod2 { get; set; }

        [JsonProperty(PropertyName = "staff_desc_2")]
        public string StaffDesc2 { get; set; }

        [JsonProperty(PropertyName = "staff_cod_3")]
        public string StaffCod3 { get; set; }

        [JsonProperty(PropertyName = "staff_desc_3")]
        public string StaffDesc3 { get; set; }

        [JsonProperty(PropertyName = "staff_cod_4")]
        public string StaffCod4 { get; set; }

        [JsonProperty(PropertyName = "staff_desc_4")]
        public string StaffDesc4 { get; set; }

        [JsonProperty(PropertyName = "staff_cod_5")]
        public string StaffCod5 { get; set; }

        [JsonProperty(PropertyName = "staff_desc_5")]
        public string StaffDesc5 { get; set; }

        [JsonProperty(PropertyName = "pred_attr_loc")]
        public string PredAttrLoc { get; set; }

        [JsonProperty(PropertyName = "pred_attr_desc_loc")]
        public string PredAttrDescLoc { get; set; }

        [JsonProperty(PropertyName = "attr_prev_cod_loc")]
        public string AttrPrevCodLoc { get; set; }

        [JsonProperty(PropertyName = "attr_prev_desc_loc")]
        public string AttrPrevDescLoc { get; set; }

        [JsonProperty(PropertyName = "perimeter_loc")]
        public string PerimeterLoc { get; set; }

        [JsonProperty(PropertyName = "perimeter_desc_loc")]
        public string PerimeterDescLoc { get; set; }

        [JsonProperty(PropertyName = "bus_line_cod_1_loc")]
        public string BusLineCod1Loc { get; set; }

        [JsonProperty(PropertyName = "bus_line_desc_1_loc")]
        public string BusLineDesc1Loc { get; set; }

        [JsonProperty(PropertyName = "bus_line_cod_2_loc")]
        public string BusLineCod2Loc { get; set; }

        [JsonProperty(PropertyName = "bus_line_desc_2_loc")]
        public string BusLineDesc2Loc { get; set; }

        [JsonProperty(PropertyName = "bus_line_cod_3_loc")]
        public string BusLineCod3Loc { get; set; }

        [JsonProperty(PropertyName = "bus_line_desc_3_loc")]
        public string BusLineDesc3Loc { get; set; }

        [JsonProperty(PropertyName = "bus_line_cod_4_loc")]
        public string BusLineCod4Loc { get; set; }

        [JsonProperty(PropertyName = "bus_line_desc_4_loc")]
        public string BusLineDesc4Loc { get; set; }

        [JsonProperty(PropertyName = "bus_line_cod_5_loc")]
        public string BusLineCod5Loc { get; set; }

        [JsonProperty(PropertyName = "bus_line_desc_5_loc")]
        public string BusLineDesc5Loc { get; set; }

        [JsonProperty(PropertyName = "serv_cod_1_loc")]
        public string ServCod1Loc { get; set; }

        [JsonProperty(PropertyName = "serv_desc_1_loc")]
        public string ServDesc1Loc { get; set; }

        [JsonProperty(PropertyName = "serv_cod_2_loc")]
        public string ServCod2Loc { get; set; }

        [JsonProperty(PropertyName = "serv_desc_2_loc")]
        public string ServDesc2Loc { get; set; }

        [JsonProperty(PropertyName = "serv_cod_3_loc")]
        public string ServCod3Loc { get; set; }

        [JsonProperty(PropertyName = "serv_desc_3_loc")]
        public string ServDesc3Loc { get; set; }

        [JsonProperty(PropertyName = "serv_cod_4_loc")]
        public string ServCod4Loc { get; set; }

        [JsonProperty(PropertyName = "serv_desc_4_loc")]
        public string ServDesc4Loc { get; set; }

        [JsonProperty(PropertyName = "serv_cod_5_loc")]
        public string ServCod5Loc { get; set; }

        [JsonProperty(PropertyName = "serv_desc_5_loc")]
        public string ServDesc5Loc { get; set; }

        [JsonProperty(PropertyName = "staff_cod_1_loc")]
        public string StaffCod1Loc { get; set; }

        [JsonProperty(PropertyName = "staff_desc_1_loc")]
        public string StaffDesc1Loc { get; set; }

        [JsonProperty(PropertyName = "staff_cod_2_loc")]
        public string StaffCod2Loc { get; set; }

        [JsonProperty(PropertyName = "staff_desc_2_loc")]
        public string StaffDesc2Loc { get; set; }

        [JsonProperty(PropertyName = "staff_cod_3_loc")]
        public string StaffCod3Loc { get; set; }

        [JsonProperty(PropertyName = "staff_desc_3_loc")]
        public string StaffDesc3Loc { get; set; }

        [JsonProperty(PropertyName = "staff_cod_4_loc")]
        public string StaffCod4Loc { get; set; }

        [JsonProperty(PropertyName = "staff_desc_4_loc")]
        public string StaffDesc4Loc { get; set; }

        [JsonProperty(PropertyName = "staff_cod_5_loc")]
        public string StaffCod5Loc { get; set; }

        [JsonProperty(PropertyName = "staff_desc_5_loc")]
        public string StaffDesc5Loc { get; set; }

        [JsonProperty(PropertyName = "u_org_cod")]
        public string uOrgCod { get; set; }

        [JsonProperty(PropertyName = "num_hc")]
        public string NumHc { get; set; }

        [JsonProperty(PropertyName = "total_cost_centers")]
        public int totalCostCenters { get; set; }


        [JsonProperty(PropertyName = "sync_date_time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? syncDateTime { get; set; }

        public HrOuBCK castHrOuBCK(HrOu hrou){
            HrOuBCK bck = new HrOuBCK();

            bck.AttrPrevCod = hrou.AttrPrevCod;
            bck.AttrPrevCodLoc = hrou.AttrPrevCodLoc;
            bck.AttrPrevDesc = hrou.AttrPrevDesc;
            bck.AttrPrevDescLoc = hrou.AttrPrevDescLoc;
            bck.BusLineCod1 = hrou.BusLineCod1;
            bck.BusLineCod1Loc = hrou.BusLineCod1Loc;
            bck.BusLineCod2 = hrou.BusLineCod2;
            bck.BusLineCod2Loc = hrou.BusLineCod2Loc;
            bck.BusLineCod3 = hrou.BusLineCod3;
            bck.BusLineCod3Loc = hrou.BusLineCod3Loc;
            bck.BusLineCod4 = hrou.BusLineCod4;
            bck.BusLineCod4Loc = hrou.BusLineCod4Loc;
            bck.BusLineCod5 = hrou.BusLineCod5;
            bck.BusLineCod5Loc = hrou.BusLineCod5Loc;
            bck.BusLineDesc1 = hrou.BusLineDesc1;
            bck.BusLineDesc1Loc = hrou.BusLineDesc1Loc;
            bck.BusLineDesc2 = hrou.BusLineDesc2;
            bck.BusLineDesc2Loc = hrou.BusLineDesc2Loc;
            bck.BusLineDesc3 = hrou.BusLineDesc3;
            bck.BusLineDesc3Loc = hrou.BusLineDesc3Loc;
            bck.BusLineDesc4 = hrou.BusLineDesc4;
            bck.BusLineDesc4Loc = hrou.BusLineDesc4Loc;
            bck.BusLineDesc5 = hrou.BusLineDesc5;
            bck.BusLineDesc5Loc = hrou.BusLineDesc5Loc;
            bck.Company = hrou.Company;
            bck.CompanyDesc = hrou.CompanyDesc;
            bck.CostCenter = hrou.CostCenter;
            bck.Country = hrou.Country;
            bck.EndDate = hrou.EndDate;
            bck.hrmdou_id = hrou.hrmdou_id;
            bck.NumHc = hrou.NumHc;
            bck.Perimeter = hrou.Perimeter;
            bck.PerimeterDesc = hrou.PerimeterDesc;
            bck.PerimeterDescLoc = hrou.PerimeterDescLoc;
            bck.PerimeterLoc = hrou.PerimeterLoc;
            bck.PredAttr = hrou.PredAttr;
            bck.PredAttrDesc = hrou.PredAttrDesc;
            bck.PredAttrDescLoc = hrou.PredAttrDescLoc  ;
            bck.PredAttrLoc = hrou.PredAttrLoc;
            bck.ServCod1 = hrou.ServCod1;
            bck.ServCod1Loc = hrou.ServCod1Loc;
            bck.ServCod2 = hrou.ServCod2;
            bck.ServCod2Loc = hrou.ServCod2Loc;
            bck.ServCod3 = hrou.ServCod3;
            bck.ServCod3Loc = hrou.ServCod3Loc;
            bck.ServCod4 = hrou.ServCod4;
            bck.ServCod4Loc = hrou.ServCod4Loc;
            bck.ServCod5 = hrou.ServCod5;
            bck.ServCod5Loc = hrou.ServCod5Loc;
            bck.ServDesc1 = hrou.ServDesc1;
            bck.ServDesc1Loc = hrou.ServDesc1Loc;
            bck.ServDesc2 = hrou.ServDesc2;
            bck.ServDesc2Loc = hrou.ServDesc2Loc;
            bck.ServDesc3 = hrou.ServDesc3;
            bck.ServDesc3Loc = hrou.ServDesc3Loc;
            bck.ServDesc4 = hrou.ServDesc4;
            bck.ServDesc4Loc = hrou.ServDesc4Loc;
            bck.ServDesc5 = hrou.ServDesc5;
            bck.ServDesc5Loc = hrou.ServDesc5Loc;
            bck.StaffCod1 = hrou.StaffCod1;
            bck.StaffCod1Loc = hrou.StaffCod1Loc;
            bck.StaffCod2 = hrou.StaffCod2;
            bck.StaffCod2Loc = hrou.StaffCod2Loc;
            bck.StaffCod3 = hrou.StaffCod3;
            bck.StaffCod3Loc = hrou.StaffCod3Loc;
            bck.StaffCod4 = hrou.StaffCod4;
            bck.StaffCod4Loc = hrou.StaffCod4Loc;
            bck.StaffCod5 = hrou.StaffCod5;
            bck.StaffCod5Loc = hrou.StaffCod5Loc;
            bck.StaffDesc1 = hrou.StaffDesc1;
            bck.StaffDesc1Loc = hrou.StaffDesc1Loc;
            bck.StaffDesc2 = hrou.StaffDesc2;
            bck.StaffDesc2Loc = hrou.StaffDesc2Loc;
            bck.StaffDesc3 = hrou.StaffDesc3;
            bck.StaffDesc3Loc = hrou.StaffDesc3Loc;
            bck.StaffDesc4 = hrou.StaffDesc4;
            bck.StaffDesc4Loc = hrou.StaffDesc4Loc;
            bck.StaffDesc5 = hrou.StaffDesc5;
            bck.StaffDesc5Loc = hrou.StaffDesc5Loc;
            bck.StartDate = hrou.StartDate;
            bck.syncDateTime = hrou.syncDateTime;
            bck.totalCostCenters = hrou.totalCostCenters;
            bck.typologyAssociation = hrou.typologyAssociation;
            bck.UOrg = hrou.UOrg;
            bck.uOrgCod = hrou.uOrgCod;
            bck.UOrgDesc = hrou.UOrgDesc;
            return bck;
        }
    }
}