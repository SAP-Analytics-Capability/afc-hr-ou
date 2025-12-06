using System;
using System.Collections.Generic;

using masterdata.Models;
using masterdata.Models.SnowCostCenterResults;

namespace masterdata.Interfaces
{
    public interface ITypologyTranscodingData
    {
        List<TypologyTranscoding> GetTypologies();
    }
}