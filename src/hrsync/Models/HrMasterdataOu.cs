using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace hrsync.Models
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

        [JsonProperty(PropertyName = "staff_cod_6")]
        public string StaffCod6 { get; set; }

        [JsonProperty(PropertyName = "staff_desc_6")]
        public string StaffDesc6 { get; set; }

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

        [JsonProperty(PropertyName = "sync_date_time")]
        public DateTime SyncDateTime { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                HrmasterdataOu ou = (HrmasterdataOu)obj;

                if (!IsEquals(this.UOrg, ou.UOrg))
                {
                    return false;
                }

                if (!IsEquals(this.UOrgDesc, ou.UOrgDesc))
                {
                    return false;
                }
                if (!IsEquals(this.Typology, ou.Typology))
                {
                    return false;
                }

                if (!IsEquals(this.StartDate, ou.StartDate) || !IsEquals(this.EndDate, ou.EndDate))
                {
                    return false;
                }

                if (!IsEquals(this.Company, ou.Company) || !IsEquals(this.CompanyDesc, ou.CompanyDesc))
                {
                    return false;
                }

                if (!IsEquals(this.PredAttr, ou.PredAttr))
                {
                    return false;
                }

                if (!IsEquals(this.AttrPrevCod, ou.AttrPrevCod))
                {
                    return false;
                }

                if (!IsEquals(this.AttrPrevDesc, ou.AttrPrevDesc))
                {
                    return false;
                }

                if (!IsEquals(this.Perimeter, ou.Perimeter) || !IsEquals(this.PerimeterDesc, ou.PerimeterDesc))
                {
                    return false;
                }

                if (!IsEquals(this.BusLineCod1, ou.BusLineCod1) || !IsEquals(this.BusLineDesc1, ou.BusLineDesc1))
                {
                    return false;
                }

                if (!IsEquals(this.BusLineCod2, ou.BusLineCod2) || !IsEquals(this.BusLineDesc2, ou.BusLineDesc2))
                {
                    return false;
                }

                if (!IsEquals(this.BusLineCod3, ou.BusLineCod3) || !IsEquals(this.BusLineDesc3, ou.BusLineDesc3))
                {
                    return false;
                }

                if (!IsEquals(this.BusLineCod4, ou.BusLineCod4) || !IsEquals(this.BusLineDesc4, ou.BusLineDesc4))
                {
                    return false;
                }

                if (!IsEquals(this.BusLineCod5, ou.BusLineCod5) || !IsEquals(this.BusLineDesc5, ou.BusLineDesc5))
                {
                    return false;
                }

                if (!IsEquals(this.ServCod1, ou.ServCod1) || !IsEquals(this.ServDesc1, ou.ServDesc1))
                {
                    return false;
                }

                if (!IsEquals(this.ServCod2, ou.ServCod2) || !IsEquals(this.ServDesc2, ou.ServDesc2))
                {
                    return false;
                }

                if (!IsEquals(this.ServCod3, ou.ServCod3) || !IsEquals(this.ServDesc3, ou.ServDesc3))
                {
                    return false;
                }

                if (!IsEquals(this.ServCod4, ou.ServCod4) || !IsEquals(this.ServDesc4, ou.ServDesc4))
                {
                    return false;
                }

                if (!IsEquals(this.ServCod5, ou.ServCod5) || !IsEquals(this.ServDesc5, ou.ServDesc5))
                {
                    return false;
                }

                if (!IsEquals(this.StaffCod1, ou.StaffCod1) || !IsEquals(this.StaffDesc1, ou.StaffDesc1))
                {
                    return false;
                }

                if (!IsEquals(this.StaffCod2, ou.StaffCod2) || !IsEquals(this.StaffDesc2, ou.StaffDesc2))
                {
                    return false;
                }

                if (!IsEquals(this.StaffCod3, ou.StaffCod3) || !IsEquals(this.StaffDesc3, ou.StaffDesc3))
                {
                    return false;
                }

                if (!IsEquals(this.StaffCod4, ou.StaffCod4) || !IsEquals(this.StaffDesc4, ou.StaffDesc4))
                {
                    return false;
                }

                if (!IsEquals(this.StaffCod5, ou.StaffCod5) || !IsEquals(this.StaffDesc5, ou.StaffDesc5))
                {
                    return false;
                }

                if (!IsEquals(this.StaffCod6, ou.StaffCod6) || !IsEquals(this.StaffDesc6, ou.StaffDesc6))
                {
                    return false;
                }

                if (!IsEquals(this.uOrgCod, ou.uOrgCod))
                {
                    return false;
                }

                if (!IsEquals(this.NumHc, ou.NumHc))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return -1;
        }
        public bool IsEquals(string a, string b)
        {
            if (a.TrimStart().TrimEnd().Equals(b.TrimStart().TrimEnd()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class HrmasterdataOuList
    {
        public List<HrmasterdataOu> hrmasterdataou { get; set; }
        public string message { get; set; }
    }
}