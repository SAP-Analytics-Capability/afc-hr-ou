using System;
using System.Collections.Generic;
using masterdata.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System.Linq;
using masterdata.Interfaces;



namespace masterdata.Helpers
{
    public class ExcelUtility : IExcelUtility
    {
        private IConfiguration Configuration;
        private ILogger Logger;
        private readonly IOptions<FileConfiguration> _AppSettingsFileConfig;
        private readonly IFaManager _FaManager;

        public ExcelUtility(
            IConfiguration configuration,
            ILoggerFactory loggerFactory,
            IOptions<FileConfiguration> fileconfig,
            IFaManager faManager)
        {
            Configuration = configuration;
            Logger = loggerFactory.CreateLogger<ExcelUtility>();
            _AppSettingsFileConfig = fileconfig;
            this._FaManager = faManager;
        }
        public bool CheckHeader(IFormFile excelFile)
        {
            List<string> headers = new List<string>();
            try
            {
                using (ExcelPackage package = new ExcelPackage(excelFile.OpenReadStream()))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = 1; // index excel parte da 1
                    int colCount = worksheet.Dimension.End.Column;

                    for (int row = 1; row <= rowCount; row++)
                    {
                        for (int col = 1; col <= colCount; col++)
                        {
                            var rowValue = worksheet.Cells[row, col].Value.ToString();
                            headers.Add(rowValue);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error while checking header: {ex.Message}");
                return false;
            }
            return _AppSettingsFileConfig.Value.ExcelHeader.ToList<string>().SequenceEqual(headers)
                && _AppSettingsFileConfig.Value.ExcelHeader.ToList<string>().Count == headers.Count;
        }

        public List<CleanHrOU> computeExcel(IFormFile excelFile)
        {
            List<CleanHrOU> dataExcel = new List<CleanHrOU>();
            try
            {
                using (ExcelPackage package = new ExcelPackage(excelFile.OpenReadStream()))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.End.Row;
                    int faId = _FaManager.GetCurrFA().idFa;

                    for (int row = 2; row <= rowCount; row++) // parte da 2 per saltare l'header
                    {
                        CleanHrOU excelData = new CleanHrOU();
                        excelData.UOrg = worksheet.Cells[row, 1].Text;
                        excelData.Company = worksheet.Cells[row, 2].Text;
                        excelData.CompanyDesc = worksheet.Cells[row, 3].Text;
                        excelData.PredAttr = worksheet.Cells[row, 4].Text;
                        excelData.AttrPrevCod = worksheet.Cells[row, 5].Text;
                        excelData.AttrPrevDesc = worksheet.Cells[row, 6].Text;
                        excelData.Perimeter = worksheet.Cells[row, 7].Text;
                        excelData.PerimeterDesc = worksheet.Cells[row, 8].Text;
                        excelData.BusLineCod1 = worksheet.Cells[row, 9].Text;
                        excelData.BusLineDesc1 = worksheet.Cells[row, 10].Text;
                        excelData.BusLineCod2 = worksheet.Cells[row, 11].Text;
                        excelData.BusLineDesc2 = worksheet.Cells[row, 12].Text;
                        excelData.BusLineCod3 = worksheet.Cells[row, 13].Text;
                        excelData.BusLineDesc3 = worksheet.Cells[row, 14].Text;
                        excelData.BusLineCod4 = worksheet.Cells[row, 15].Text;
                        excelData.BusLineDesc4 = worksheet.Cells[row, 16].Text;
                        excelData.BusLineCod5 = worksheet.Cells[row, 17].Text;
                        excelData.BusLineDesc5 = worksheet.Cells[row, 18].Text;
                        excelData.ServCod1 = worksheet.Cells[row, 19].Text;
                        excelData.ServDesc1 = worksheet.Cells[row, 20].Text;
                        excelData.ServCod2 = worksheet.Cells[row, 21].Text;
                        excelData.ServDesc2 = worksheet.Cells[row, 22].Text;
                        excelData.ServCod3 = worksheet.Cells[row, 23].Text;
                        excelData.ServDesc3 = worksheet.Cells[row, 24].Text;
                        excelData.ServCod4 = worksheet.Cells[row, 25].Text;
                        excelData.ServDesc4 = worksheet.Cells[row, 26].Text;
                        excelData.ServCod5 = worksheet.Cells[row, 27].Text;
                        excelData.ServDesc5 = worksheet.Cells[row, 28].Text;
                        excelData.StaffCod1 = worksheet.Cells[row, 29].Text;
                        excelData.StaffDesc1 = worksheet.Cells[row, 30].Text;
                        excelData.StaffCod2 = worksheet.Cells[row, 31].Text;
                        excelData.StaffDesc2 = worksheet.Cells[row, 32].Text;
                        excelData.StaffCod3 = worksheet.Cells[row, 33].Text;
                        excelData.StaffDesc3 = worksheet.Cells[row, 34].Text;
                        excelData.StaffCod4 = worksheet.Cells[row, 35].Text;
                        excelData.StaffDesc4 = worksheet.Cells[row, 36].Text;
                        excelData.StaffCod5 = worksheet.Cells[row, 37].Text;
                        excelData.StaffDesc5 = worksheet.Cells[row, 38].Text;
                        excelData.faId = faId;
                        dataExcel.Add(excelData);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error while computing excel rows: {ex.Message}");
            }
            return dataExcel;
        }
    }
}