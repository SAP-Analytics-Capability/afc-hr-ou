using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace rulesmngt.Models
{
    public class ConstantValue
    {
        [Key]
        [JsonProperty(PropertyName = "constant_id")]
        public Int32 constantId { get; set; }

        [JsonProperty(PropertyName = "EnelItalia")]
        public string enelitalia { get; set; }

        [JsonProperty(PropertyName = "Italiaper")]
        public string italiaper { get; set; }

        [JsonProperty(PropertyName = "CentralperDesc")]
        public string centralperdesc { get; set; }

        [JsonProperty(PropertyName = "ItaliaperDesc")]
        public string italiaperdesc { get; set; }

        [JsonProperty(PropertyName = "enelproduzione")]
        public string enelproduzione { get; set; }

        [JsonProperty(PropertyName = "attrprevbsml")]
        public string attrPrevBsMl { get; set; }

        [JsonProperty(PropertyName = "gds")]
        public string gds { get; set; }

        [JsonProperty(PropertyName = "sesec")]
        public string sesec { get; set; }

        [JsonProperty(PropertyName = "dighub")]
        public string dighub { get; set; }

        [JsonProperty(PropertyName = "peo")]
        public string peo { get; set; }

        [JsonProperty(PropertyName = "chapter")]
        public string chapter { get; set; }

        [JsonProperty(PropertyName = "digfac")]
        public string digfac { get; set; }

        [JsonProperty(PropertyName = "procurement")]
        public string procurement { get; set; }

        [JsonProperty(PropertyName = "communications")]
        public string communications { get; set; }

        [JsonProperty(PropertyName = "afc")]
        public string afc { get; set; }

        [JsonProperty(PropertyName = "businesslinecode")]
        public string businesslinecode { get; set; }

        [JsonProperty(PropertyName = "staffcode")]
        public string staffcode { get; set; }

        [JsonProperty(PropertyName = "servicecode")]
        public string servicecode { get; set; }

        [JsonProperty(PropertyName = "iHOL")]
        public string ihol { get; set; }

        [JsonProperty(PropertyName = "iSER")]
        public string iser { get; set; }

        [JsonProperty(PropertyName = "bHOL")]
        public string bhol { get; set; }

        [JsonProperty(PropertyName = "bSER")]
        public string bser { get; set; }

        [JsonProperty(PropertyName = "gbl")]
        public string gbl { get; set; }

        [JsonProperty(PropertyName = "bholbser")]
        public string bholBser { get; set; }

        [JsonProperty(PropertyName = "gblbholbser")]
        public string gblBholBser { get; set; }

        [JsonProperty(PropertyName = "rel")]
        public string rel { get; set; }

        [JsonProperty(PropertyName = "ret")]
        public string ret { get; set; }

        [JsonProperty(PropertyName = "esl")]
        public string esl { get; set; }

        [JsonProperty(PropertyName = "eso")]
        public string eso { get; set; }

        [JsonProperty(PropertyName = "iRel")]
        public string irel { get; set; }

        [JsonProperty(PropertyName = "iRet")]
        public string iret { get; set; }

        [JsonProperty(PropertyName = "iEsl")]
        public string iesl { get; set; }

        [JsonProperty(PropertyName = "iEso")]
        public string ieso { get; set; }

        [JsonProperty(PropertyName = "ficticious")]
        public string ficticious { get; set; }

        [JsonProperty(PropertyName = "gco")]
        public string gco { get; set; }

        [JsonProperty(PropertyName = "smd")]
        public string smd { get; set; }

        [JsonProperty(PropertyName = "gat")]
        public string gat { get; set; }

        [JsonProperty(PropertyName = "gbi")]
        public string gbi { get; set; }

        [JsonProperty(PropertyName = "gcc")]
        public string gcc { get; set; }

        [JsonProperty(PropertyName = "gch")]
        public string gch { get; set; }

        [JsonProperty(PropertyName = "gcr")]
        public string gcr { get; set; }

        [JsonProperty(PropertyName = "gcs")]
        public string gcs { get; set; }

        [JsonProperty(PropertyName = "gfr")]
        public string gfr { get; set; }

        [JsonProperty(PropertyName = "goi")]
        public string goi { get; set; }

        [JsonProperty(PropertyName = "gpq")]
        public string gpq { get; set; }

        [JsonProperty(PropertyName = "gco_constants")]
        public string gcoConstants { get; set; }

        [JsonProperty(PropertyName = "gco_abbr_constants")]
        public string gcoAbbrConstants { get; set; }

    }
}
