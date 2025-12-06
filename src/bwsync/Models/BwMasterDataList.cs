using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace bwsync.Models
{
    public class Result
    {
        public Metadata __metadata { get; set; }

        [JsonProperty(PropertyName = "ID")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "TotaledProperties")]
        public string TotaledProperties { get; set; }

        [JsonProperty(PropertyName = "A4ZFICP12SOURCE_SYSTE")]
        public string A4ZFICP12Source_System { get; set; }

        [JsonProperty(PropertyName = "YCOAREA")]
        public string YCoArea { get; set; }

        [JsonProperty(PropertyName = "YCOAREA_T")]
        public string YCoArea_T { get; set; }

        [JsonProperty(PropertyName = "A4ZFICP12KOSTL")]
        public string A4ZFICP12KOSTL { get; set; }

        [JsonProperty(PropertyName = "A4ZFICP12LTEXT_CDC")]
        public string A4ZFICP12LText_CDC { get; set; }

        [JsonProperty(PropertyName = "A0DATETO")]
        public string A0DateTo { get; set; }

        [JsonProperty(PropertyName = "A0DATETO_T")]
        public string A0DateTo_T { get; set; }

        [JsonProperty(PropertyName = "A0DATEFROM")]
        public string A0DateFrom { get; set; }

        [JsonProperty(PropertyName = "A0DATEFROM_T")]
        public string A0DateFrom_T { get; set; }

        [JsonProperty(PropertyName = "YCOMPCODE")]
        public string YCompCode { get; set; }

        [JsonProperty(PropertyName = "YCOMPCODE_T")]
        public string YCompCode_T { get; set; }

        [JsonProperty(PropertyName = "A4ZFICP12LOGSYSTEM")]
        public string A4ZFICP12LOGSYSTEM { get; set; }

        [JsonProperty(PropertyName = "A0CREATEDON")]
        public string A0CreateDON { get; set; }

        [JsonProperty(PropertyName = "A0CREATEDON_T")]
        public string A0CreateDON_T { get; set; }

        [JsonProperty(PropertyName = "A4ZFICP12ZZPROC")]
        public string A4ZFICP12ZZPROC { get; set; }

        [JsonProperty(PropertyName = "A4ZFICP12ZZCOD_ORG")]
        public string A4ZFICP12ZZCOD_Org { get; set; }

        [JsonProperty(PropertyName = "A4ZFICP12ZZORGUN")]
        public string A4ZFICP12ZZORGUN { get; set; }

        [JsonProperty(PropertyName = "A4ZFICP12PRCTR")]
        public string A4ZFICP12PRCTR { get; set; }

        [JsonProperty(PropertyName = "A4ZFICP12LTEXT_CDP")]
        public string A4ZFICP12LTEXT_CDP { get; set; }

        [JsonProperty(PropertyName = "YPRIMRESP")]
        public string YPRIMResp { get; set; }

        [JsonProperty(PropertyName = "YVALCHAIN")]
        public string YVALChain { get; set; }

        [JsonProperty(PropertyName = "YVALCHAIN_T")]
        public string YVALChain_T { get; set; }

        [JsonProperty(PropertyName = "ZORGUN")]
        public string ZORGUN { get; set; }

        [JsonProperty(PropertyName = "ZMACRO2")]
        public string ZMacro2 { get; set; }

        [JsonProperty(PropertyName = "ZMACRO1")]
        public string ZMacro1 { get; set; }

        [JsonProperty(PropertyName = "ZCODEORG")]
        public string ZCodeOrg { get; set; }

        [JsonProperty(PropertyName = "ZKOSAR")]
        public string ZKOSAR { get; set; }

        [JsonProperty(PropertyName = "A4ZFICP12LAND1")]
        public string A4ZFICP12LAND1 { get; set; }

        [JsonProperty(PropertyName = "A4ZFICP12ZTIPOCC")]
        public string A4ZFICP12ZTIPOCC { get; set; }

        [JsonProperty(PropertyName = "ZABTEI")]
        public string ZABTEI { get; set; }
        



        public Parameters Parameters { get; set; }
    }

    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Deferred
    {
        public string uri { get; set; }
    }

    public class Parameters
    {
        public Deferred __deferred { get; set; }
    }

    public class D
    {
        public List<Result> results { get; set; }
    }

    public class BwMasterDataList
    {
        public D d { get; set; }
    }

}