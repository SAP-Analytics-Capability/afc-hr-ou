using System;
using System.Collections.Generic;
using masterdata.Models.SnowCostCenterResults;

namespace masterdata.Interfaces
{
    public interface IResultAdapter
    {
        void InsertResultsByOUCodAndCCCod(RootObject results);
        void InsertResultsByOUCodAndCCCod(RootObject results, DateTime syncdatetime);
    }
}