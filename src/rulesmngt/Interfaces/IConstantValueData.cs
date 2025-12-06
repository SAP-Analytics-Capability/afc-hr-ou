using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IConstantValueData
    {
        List<ConstantValue> GetConstants();
        ConstantValue GetConstant();
    }
}