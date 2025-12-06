using System;
using System.Collections.Generic;

namespace bwsync.Models.Sap
{
    public class Result
    {
        public string u_costcenter { get; set; }
        public string u_ou_code { get; set; }
    }

    public class BwSapDataList
    {
        public List<Result> result { get; set; }
    }
}