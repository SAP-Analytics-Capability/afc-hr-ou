using System;
using System.ComponentModel.DataAnnotations;

namespace masterdata.Models
{
    public class ClientAccess
    {
        [Key]
        public int AccessId { get; set; }
        public string Username { get; set; }
        public DateTime AccessTimestamp { get; set; }
        public string AccessType { get; set; }
        public string Status { get; set; }
        public DateTime? ExitTimestamp { get; set; }
    }
}