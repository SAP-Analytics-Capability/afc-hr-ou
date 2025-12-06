using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IExceptionTableData
    {
        void InsertException(ExceptionTable exeption);

        List<ExceptionTable> GetExceptions(); 

        ExceptionTable GetExceptionByTypoUo(string tipoUo);

        List<ExceptionTable> GetExceptionByTypoUoGblPrev(string typoUo, string gblPrevalent);

    }
}