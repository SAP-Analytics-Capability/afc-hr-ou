using System;
using System.Collections.Generic;
using masterdata.Models;

namespace masterdata.Interfaces
{
    public interface ICleanHrOU
    {
        void AddNewExcelData(List<CleanHrOU> masterExcel);
        List<CleanHrOU> RetrieveDataByFa(int faId);
        void AddNewTxTData(List<string> TxT, int faId);
    }
}

