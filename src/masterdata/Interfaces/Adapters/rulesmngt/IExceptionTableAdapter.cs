using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using masterdata.Models;

namespace masterdata.Interfaces
{
    public interface IExceptionTableAdapter
    {
        Task<List<ExceptionTable>> GetExceptions(); 

        Task<ExceptionTable> GetExceptionByTypoUo(CancellationToken cancellationToken, string tipoUo);

        Task<List<ExceptionTable>> GetExceptionByTypoUoGblPrev(CancellationToken cancellationToken, string typoUo, string gblPrevalent);

    }
}