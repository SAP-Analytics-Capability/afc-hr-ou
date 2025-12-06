using System;
using System.Collections.Generic;

namespace snowsync.Models
{
    public class Result
    {
        public string u_costcenter { get; set; }
        public string u_ou_code { get; set; }
    }

    public class RootObject
    {
        public List<Result> result { get; set; }
    }
}