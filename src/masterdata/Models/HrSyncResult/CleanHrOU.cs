using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace masterdata.Models
{
    public class CleanHrOU
    {
        [Key]
        // [JsonProperty(PropertyName = "hrmdou_id")]
        public int hrmdou_id { get; set; }

        [JsonProperty(PropertyName = "u_org")]
        public string UOrg { get; set; }

        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }


        [JsonProperty(PropertyName = "company_desc")]
        public string CompanyDesc { get; set; }


        [JsonProperty(PropertyName = "pred_attr")]
        public string PredAttr { get; set; }


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


        [JsonProperty(PropertyName = "fa_id")]

        public int faId { get; set; }

        public CleanHrOU Clone(object obj)
         {
             HrmasterdataOu hrdata = (HrmasterdataOu)obj;
             this.hrmdou_id = hrdata.hrmdou_id;
             this.UOrg = hrdata.UOrg;
             this.Company = hrdata.Company;
             this.CompanyDesc = hrdata.CompanyDesc;
             this.PredAttr = hrdata.PredAttr;
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
             return (CleanHrOU)(object)this;
         }
    }
}