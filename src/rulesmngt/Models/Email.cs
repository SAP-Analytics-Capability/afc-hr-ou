using System;
using System.ComponentModel.DataAnnotations;

namespace rulesmngt.Models
{
    public class Email
    {
        [Key]
        public string mail {get; set;}
        public string address_type {get; set;}
        public DateTime due_date { get; set; }
    }
}