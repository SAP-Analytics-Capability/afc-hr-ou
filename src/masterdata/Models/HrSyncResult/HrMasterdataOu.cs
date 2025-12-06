using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace masterdata.Models
{
    public class HrmasterdataOu
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

        public HrmasterdataOu() { }

        public HrmasterdataOu(string fake)
        {

            this.UOrg = "u_org";
            this.UOrgDesc = fake;
            this.StartDate = "start_date";
            this.EndDate = "end_date";
            this.Company = "company";
            this.CompanyDesc = "company_desc";
            this.CostCenter = "cost_center";
            this.PredAttr = "pred_attr";
            this.PredAttrDesc = "pred_attr_desc";
            this.AttrPrevCod = "attr_prev_cod";
            this.AttrPrevDesc = "attr_prev_desc";
            this.Perimeter = "perimeter";
            this.PerimeterDesc = "perimeter_desc";
            this.BusLineCod1 = "bus_line_cod_1";
            this.BusLineDesc1 = "bus_line_desc_1";
            this.BusLineCod2 = "bus_line_cod_2";
            this.BusLineDesc2 = "bus_line_desc_2";
            this.BusLineCod3 = "bus_line_cod_3";
            this.BusLineDesc3 = "bus_line_desc_3";
            this.BusLineCod4 = "bus_line_cod_4";
            this.BusLineDesc4 = "bus_line_desc_4";
            this.BusLineCod5 = "bus_line_cod_5";
            this.BusLineDesc5 = "bus_line_desc_5";
            this.ServCod1 = "serv_cod_1";
            this.ServDesc1 = "serv_desc_1";
            this.ServCod2 = "serv_cod_2";
            this.ServDesc2 = "serv_desc_2";
            this.ServCod3 = "serv_cod_3";
            this.ServDesc3 = "serv_desc_3";
            this.ServCod4 = "serv_cod_4";
            this.ServDesc4 = "serv_desc_4";
            this.ServCod5 = "serv_cod_5";
            this.ServDesc5 = "serv_desc_5";
            this.StaffCod1 = "staff_cod_1";
            this.StaffDesc1 = "staff_desc_1";
            this.StaffCod2 = "staff_cod_2";
            this.StaffDesc2 = "staff_desc_2";
            this.StaffCod3 = "staff_cod_3";
            this.StaffDesc3 = "staff_desc_3";
            this.StaffCod4 = "staff_cod_4";
            this.StaffDesc4 = "staff_desc_4";
            this.StaffCod5 = "staff_cod_5";
            this.StaffDesc5 = "staff_desc_5";
            this.PredAttrLoc = "pred_attr_loc";
            this.PredAttrDescLoc = "pred_attr_desc_loc";
            this.AttrPrevCodLoc = "attr_prev_cod_loc";
            this.AttrPrevDescLoc = "attr_prev_desc_loc";
            this.PerimeterLoc = "perimeter_loc";
            this.PerimeterDescLoc = "perimeter_desc_loc";
            this.BusLineCod1Loc = "bus_line_cod_1_loc";
            this.BusLineDesc1Loc = "bus_line_desc_1_loc";
            this.BusLineCod2Loc = "bus_line_cod_2_loc";
            this.BusLineDesc2Loc = "bus_line_desc_2_loc";
            this.BusLineCod3Loc = "bus_line_cod_3_loc";
            this.BusLineDesc3Loc = "bus_line_desc_3_loc";
            this.BusLineCod4Loc = "bus_line_cod_4_loc";
            this.BusLineDesc4Loc = "bus_line_desc_4_loc";
            this.BusLineCod5Loc = "bus_line_cod_5_loc";
            this.BusLineDesc5Loc = "bus_line_desc_5_loc";
            this.ServCod1Loc = "serv_cod_1_loc";
            this.ServDesc1Loc = "serv_desc_1_loc";
            this.ServCod2Loc = "serv_cod_2_loc";
            this.ServDesc2Loc = "serv_desc_2_loc";
            this.ServCod3Loc = "serv_cod_3_loc";
            this.ServDesc3Loc = "serv_desc_3_loc";
            this.ServCod4Loc = "serv_cod_4_loc";
            this.ServDesc4Loc = "serv_desc_4_loc";
            this.ServCod5Loc = "serv_cod_5_loc";
            this.ServDesc5Loc = "serv_desc_5_loc";
            this.StaffCod1Loc = "staff_cod_1_loc";
            this.StaffDesc1Loc = "staff_desc_1_loc";
            this.StaffCod2Loc = "staff_cod_2_loc";
            this.StaffDesc2Loc = "staff_desc_2_loc";
            this.StaffCod3Loc = "staff_cod_3_loc";
            this.StaffDesc3Loc = "staff_desc_3_loc";
            this.StaffCod4Loc = "staff_cod_4_loc";
            this.StaffDesc4Loc = "staff_desc_4_loc";
            this.StaffCod5Loc = "staff_cod_5_loc";
            this.StaffDesc5Loc = "staff_desc_5_loc";
            this.uOrgCod = "u_org_cod";
            this.NumHc = "num_hc";
        }

        public HrmasterdataOu CloneMaster(object obj)
        {
            CleanHrOU hrdata = (CleanHrOU)obj;

            this.UOrg = hrdata.UOrg;
            this.UOrgDesc = "";
            this.StartDate = "";
            this.EndDate = "";
            this.Country = "";
            this.Company = hrdata.Company;
            this.CompanyDesc = hrdata.CompanyDesc;
            this.CostCenter = "";
            this.PredAttr = hrdata.PredAttr;
            this.PredAttrDesc = "";
            this.AttrPrevCod = hrdata.AttrPrevCod;
            this.AttrPrevDesc = hrdata.AttrPrevDesc;
            this.Perimeter = hrdata.Perimeter;
            this.PerimeterDesc = hrdata.PerimeterDesc;
            this.BusLineCod1 = hrdata.BusLineCod1;
            this.BusLineDesc1 = hrdata.BusLineDesc1;
            this.BusLineCod2 = hrdata.BusLineCod2;
            this.BusLineDesc2 = hrdata.BusLineDesc2;
            this.BusLineCod3 = hrdata.BusLineCod3;
            this.BusLineDesc3 = hrdata.BusLineDesc3;
            this.BusLineCod4 = hrdata.BusLineCod4;
            this.BusLineDesc4 = hrdata.BusLineDesc4;
            this.BusLineCod5 = hrdata.BusLineCod5;
            this.BusLineDesc5 = hrdata.BusLineDesc5;
            this.ServCod1 = hrdata.ServCod1;
            this.ServDesc1 = hrdata.ServDesc1;
            this.ServCod2 = hrdata.ServCod2;
            this.ServDesc2 = hrdata.ServDesc2;
            this.ServCod3 = hrdata.ServCod3;
            this.ServDesc3 = hrdata.ServDesc3;
            this.ServCod4 = hrdata.ServCod4;
            this.ServDesc4 = hrdata.ServDesc4;
            this.ServCod5 = hrdata.ServCod5;
            this.ServDesc5 = hrdata.ServDesc5;
            this.StaffCod1 = hrdata.StaffCod1;
            this.StaffDesc1 = hrdata.StaffDesc1;
            this.StaffCod2 = hrdata.StaffCod2;
            this.StaffDesc2 = hrdata.StaffDesc2;
            this.StaffCod3 = hrdata.StaffCod3;
            this.StaffDesc3 = hrdata.StaffDesc3;
            this.StaffCod4 = hrdata.StaffCod4;
            this.StaffDesc4 = hrdata.StaffDesc4;
            this.StaffCod5 = hrdata.StaffCod5;
            this.StaffDesc5 = hrdata.StaffDesc5;
            this.PredAttrLoc = "";
            this.PredAttrDescLoc = "";
            this.AttrPrevCodLoc = "";
            this.AttrPrevDescLoc = "";
            this.PerimeterLoc = "";
            this.PerimeterDescLoc ="";;
            this.BusLineCod1Loc = "";
            this.BusLineDesc1Loc ="";
            this.BusLineCod2Loc = "";
            this.BusLineDesc2Loc ="";
            this.BusLineCod3Loc = "";
            this.BusLineDesc3Loc ="";
            this.BusLineCod4Loc = "";
            this.BusLineDesc4Loc ="";
            this.BusLineCod5Loc = "";
            this.BusLineDesc5Loc ="";
            this.ServCod1Loc = "";
            this.ServDesc1Loc = "";
            this.ServCod2Loc = "";
            this.ServDesc2Loc = "";
            this.ServCod3Loc = "";
            this.ServDesc3Loc = "";
            this.ServCod4Loc = "";
            this.ServDesc4Loc = "";
            this.ServCod5Loc = "";
            this.ServDesc5Loc = "";
            this.StaffCod1Loc = "";
            this.StaffDesc1Loc ="";
            this.StaffCod2Loc = "";
            this.StaffDesc2Loc ="";
            this.StaffCod3Loc = "";
            this.StaffDesc3Loc ="";
            this.StaffCod4Loc = "";
            this.StaffDesc4Loc ="";
            this.StaffCod5Loc = "";
            this.StaffDesc5Loc = "";
            this.uOrgCod = "";
            this.NumHc = "";
            //this.SyncDateTime = hrdata.SyncDateTime;
            return (HrmasterdataOu)(object)this;
        }
    }
}