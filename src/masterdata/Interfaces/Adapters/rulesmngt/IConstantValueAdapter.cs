using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using masterdata.Helpers;
using masterdata.Models;

namespace masterdata.Interfaces
{
    public interface IConstantValueAdapter
    {
        //Task<List<ConstantValue>> GetExceptions(); 

        Task<ConstantValue> GetConstantValue(CancellationToken cancellationToken);

        //Task<List<ExceptionTable>> GetExceptionByTypoUoGblPrev(CancellationToken cancellationToken, string typoUo, string gblPrevalent);

    }
}