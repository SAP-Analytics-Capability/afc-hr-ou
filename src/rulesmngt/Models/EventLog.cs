using System;
using System.ComponentModel.DataAnnotations;

namespace rulesmngt.Models
{
    public class EventLog
    {
        [Key]
        public int EventId { get; set; }
        public string Message { get; set; }
        public string TypeInfo { get; set; }
        public string Operation { get; set; }
        public DateTime MessageDateTime { get; set; }
    }
}