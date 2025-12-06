using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace masterdata.Models
{
    public class HrOu
    {
        //[JsonIgnore]
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
        public HrOu Clone(object obj)
        {
            HrmasterdataOu hrdata = (HrmasterdataOu)obj;

            this.typologyAssociation = hrdata.Typology;
            this.UOrg = hrdata.UOrg;
            this.UOrgDesc = hrdata.UOrgDesc;
            this.StartDate = hrdata.StartDate;
            this.EndDate = hrdata.EndDate;
            this.Country = hrdata.Country;
            this.Company = hrdata.Company;
            this.CompanyDesc = hrdata.CompanyDesc;
            this.CostCenter = hrdata.CostCenter;
            this.PredAttr = hrdata.PredAttr;
            this.PredAttrDesc = hrdata.PredAttrDesc;
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
            this.PredAttrLoc = hrdata.PredAttrLoc;
            this.PredAttrDescLoc = hrdata.PredAttrDescLoc;
            this.AttrPrevCodLoc = hrdata.AttrPrevCodLoc;
            this.AttrPrevDescLoc = hrdata.AttrPrevDescLoc;
            this.PerimeterLoc = hrdata.PerimeterLoc;
            this.PerimeterDescLoc = hrdata.PerimeterDescLoc;
            this.BusLineCod1Loc = hrdata.BusLineCod1Loc;
            this.BusLineDesc1Loc = hrdata.BusLineDesc1Loc;
            this.BusLineCod2Loc = hrdata.BusLineCod2Loc;
            this.BusLineDesc2Loc = hrdata.BusLineDesc2Loc;
            this.BusLineCod3Loc = hrdata.BusLineCod3Loc;
            this.BusLineDesc3Loc = hrdata.BusLineDesc3Loc;
            this.BusLineCod4Loc = hrdata.BusLineCod4Loc;
            this.BusLineDesc4Loc = hrdata.BusLineDesc4Loc;
            this.BusLineCod5Loc = hrdata.BusLineCod5Loc;
            this.BusLineDesc5Loc = hrdata.BusLineDesc5Loc;
            this.ServCod1Loc = hrdata.ServCod1Loc;
            this.ServDesc1Loc = hrdata.ServDesc1Loc;
            this.ServCod2Loc = hrdata.ServCod2Loc;
            this.ServDesc2Loc = hrdata.ServDesc2Loc;
            this.ServCod3Loc = hrdata.ServCod3Loc;
            this.ServDesc3Loc = hrdata.ServDesc3Loc;
            this.ServCod4Loc = hrdata.ServCod4Loc;
            this.ServDesc4Loc = hrdata.ServDesc4Loc;
            this.ServCod5Loc = hrdata.ServCod5Loc;
            this.ServDesc5Loc = hrdata.ServDesc5Loc;
            this.StaffCod1Loc = hrdata.StaffCod1Loc;
            this.StaffDesc1Loc = hrdata.StaffDesc1Loc;
            this.StaffCod2Loc = hrdata.StaffCod2Loc;
            this.StaffDesc2Loc = hrdata.StaffDesc2Loc;
            this.StaffCod3Loc = hrdata.StaffCod3Loc;
            this.StaffDesc3Loc = hrdata.StaffDesc3Loc;
            this.StaffCod4Loc = hrdata.StaffCod4Loc;
            this.StaffDesc4Loc = hrdata.StaffDesc4Loc;
            this.StaffCod5Loc = hrdata.StaffCod5Loc;
            this.StaffDesc5Loc = hrdata.StaffDesc5Loc;
            this.uOrgCod = hrdata.uOrgCod;
            this.NumHc = hrdata.NumHc;
            return (HrOu)(object)this;
        }


    }
}