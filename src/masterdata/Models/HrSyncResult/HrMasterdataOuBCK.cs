using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace masterdata.Models.HrSyncResult
{
    public class HrMasterdataOuBCK
    {
     [Key]
        [JsonProperty(PropertyName = "hrmdou_id")]
        public int hrmdou_id { get; set; }
        
        [JsonProperty(PropertyName = "typology_association")]
        public string Typology { get; set; }

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

        [JsonIgnore]
        [JsonProperty(PropertyName = "sync_date_time")]
        public DateTime SyncDateTime { get; set; }

        public HrMasterdataOuBCK CastHrMasterOu(HrmasterdataOu hrmaster){
            HrMasterdataOuBCK bck = new HrMasterdataOuBCK();

             bck.AttrPrevCod = hrmaster.AttrPrevCod;
            bck.AttrPrevCodLoc = hrmaster.AttrPrevCodLoc;
            bck.AttrPrevDesc = hrmaster.AttrPrevDesc;
            bck.AttrPrevDescLoc = hrmaster.AttrPrevDescLoc;
            bck.BusLineCod1 = hrmaster.BusLineCod1;
            bck.BusLineCod1Loc = hrmaster.BusLineCod1Loc;
            bck.BusLineCod2 = hrmaster.BusLineCod2;
            bck.BusLineCod2Loc = hrmaster.BusLineCod2Loc;
            bck.BusLineCod3 = hrmaster.BusLineCod3;
            bck.BusLineCod3Loc = hrmaster.BusLineCod3Loc;
            bck.BusLineCod4 = hrmaster.BusLineCod4;
            bck.BusLineCod4Loc = hrmaster.BusLineCod4Loc;
            bck.BusLineCod5 = hrmaster.BusLineCod5;
            bck.BusLineCod5Loc = hrmaster.BusLineCod5Loc;
            bck.BusLineDesc1 = hrmaster.BusLineDesc1;
            bck.BusLineDesc1Loc = hrmaster.BusLineDesc1Loc;
            bck.BusLineDesc2 = hrmaster.BusLineDesc2;
            bck.BusLineDesc2Loc = hrmaster.BusLineDesc2Loc;
            bck.BusLineDesc3 = hrmaster.BusLineDesc3;
            bck.BusLineDesc3Loc = hrmaster.BusLineDesc3Loc;
            bck.BusLineDesc4 = hrmaster.BusLineDesc4;
            bck.BusLineDesc4Loc = hrmaster.BusLineDesc4Loc;
            bck.BusLineDesc5 = hrmaster.BusLineDesc5;
            bck.BusLineDesc5Loc = hrmaster.BusLineDesc5Loc;
            bck.Company = hrmaster.Company;
            bck.CompanyDesc = hrmaster.CompanyDesc;
            bck.CostCenter = hrmaster.CostCenter;
            bck.Country = hrmaster.Country;
            bck.EndDate = hrmaster.EndDate;
            bck.hrmdou_id = hrmaster.hrmdou_id;
            bck.NumHc = hrmaster.NumHc;
            bck.Perimeter = hrmaster.Perimeter;
            bck.PerimeterDesc = hrmaster.PerimeterDesc;
            bck.PerimeterDescLoc = hrmaster.PerimeterDescLoc;
            bck.PerimeterLoc = hrmaster.PerimeterLoc;
            bck.PredAttr = hrmaster.PredAttr;
            bck.PredAttrDesc = hrmaster.PredAttrDesc;
            bck.PredAttrDescLoc = hrmaster.PredAttrDescLoc  ;
            bck.PredAttrLoc = hrmaster.PredAttrLoc;
            bck.ServCod1 = hrmaster.ServCod1;
            bck.ServCod1Loc = hrmaster.ServCod1Loc;
            bck.ServCod2 = hrmaster.ServCod2;
            bck.ServCod2Loc = hrmaster.ServCod2Loc;
            bck.ServCod3 = hrmaster.ServCod3;
            bck.ServCod3Loc = hrmaster.ServCod3Loc;
            bck.ServCod4 = hrmaster.ServCod4;
            bck.ServCod4Loc = hrmaster.ServCod4Loc;
            bck.ServCod5 = hrmaster.ServCod5;
            bck.ServCod5Loc = hrmaster.ServCod5Loc;
            bck.ServDesc1 = hrmaster.ServDesc1;
            bck.ServDesc1Loc = hrmaster.ServDesc1Loc;
            bck.ServDesc2 = hrmaster.ServDesc2;
            bck.ServDesc2Loc = hrmaster.ServDesc2Loc;
            bck.ServDesc3 = hrmaster.ServDesc3;
            bck.ServDesc3Loc = hrmaster.ServDesc3Loc;
            bck.ServDesc4 = hrmaster.ServDesc4;
            bck.ServDesc4Loc = hrmaster.ServDesc4Loc;
            bck.ServDesc5 = hrmaster.ServDesc5;
            bck.ServDesc5Loc = hrmaster.ServDesc5Loc;
            bck.StaffCod1 = hrmaster.StaffCod1;
            bck.StaffCod1Loc = hrmaster.StaffCod1Loc;
            bck.StaffCod2 = hrmaster.StaffCod2;
            bck.StaffCod2Loc = hrmaster.StaffCod2Loc;
            bck.StaffCod3 = hrmaster.StaffCod3;
            bck.StaffCod3Loc = hrmaster.StaffCod3Loc;
            bck.StaffCod4 = hrmaster.StaffCod4;
            bck.StaffCod4Loc = hrmaster.StaffCod4Loc;
            bck.StaffCod5 = hrmaster.StaffCod5;
            bck.StaffCod5Loc = hrmaster.StaffCod5Loc;
            bck.StaffDesc1 = hrmaster.StaffDesc1;
            bck.StaffDesc1Loc = hrmaster.StaffDesc1Loc;
            bck.StaffDesc2 = hrmaster.StaffDesc2;
            bck.StaffDesc2Loc = hrmaster.StaffDesc2Loc;
            bck.StaffDesc3 = hrmaster.StaffDesc3;
            bck.StaffDesc3Loc = hrmaster.StaffDesc3Loc;
            bck.StaffDesc4 = hrmaster.StaffDesc4;
            bck.StaffDesc4Loc = hrmaster.StaffDesc4Loc;
            bck.StaffDesc5 = hrmaster.StaffDesc5;
            bck.StaffDesc5Loc = hrmaster.StaffDesc5Loc;
            bck.StartDate = hrmaster.StartDate;
            bck.SyncDateTime = hrmaster.SyncDateTime;
            bck.Typology = hrmaster.Typology;
            bck.UOrg = hrmaster.UOrg;
            bck.uOrgCod = hrmaster.uOrgCod;
            bck.UOrgDesc = hrmaster.UOrgDesc;
            return bck;
        }
    }
}