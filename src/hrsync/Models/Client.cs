using System;
using System.ComponentModel.DataAnnotations;

namespace hrsync.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string Application { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Enabled { get; set; }
    }
}