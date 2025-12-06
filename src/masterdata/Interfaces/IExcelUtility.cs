using masterdata.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic; 

namespace masterdata.Interfaces
{
    public interface IExcelUtility
    {
        bool CheckHeader(IFormFile excelFile);

        List<CleanHrOU> computeExcel(IFormFile excelFile);
    }
}