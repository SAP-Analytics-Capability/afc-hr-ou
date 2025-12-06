using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace masterdata.Models
{
    public class TypologyTranscoding
    {

        [Key]
        public int RecordId {get; set;}
        public string SnowCode { get; set; }
        public string ParentName { get; set; }
        public string AttributeName { get; set; }
    }
}