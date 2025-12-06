using System;
using masterdata.Interfaces;
using masterdata.Models;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Collections.Generic;






namespace masterdata.Helpers
{
    public class CheckFileConfiguration : ICheckFileConfiguration
    {
        private IConfiguration Configuration;
        private ILogger Logger;
        private readonly IOptions<FileConfiguration> _FileConfig;
        private readonly IExcelUtility _ExcelUtility;

        public CheckFileConfiguration(
            IConfiguration configuration,
            ILoggerFactory loggerFactory,
            IOptions<FileConfiguration> fileconfig,
            IExcelUtility excelUtility)
        {
            Configuration = configuration;
            Logger = loggerFactory.CreateLogger<CheckFileConfiguration>();
            _FileConfig = fileconfig;
            this._ExcelUtility = excelUtility;
        }
        public bool CheckConfiguration(IFormFile file, out List<string> resultTxT)
        {
            resultTxT = new List<string>();
            bool checkconfig = false;
            int i = 0;
            try
            {
                if (Path.GetExtension(file.FileName).Equals(FaConstants.xlsx)
                    && file.FileName.Equals(_FileConfig.Value.FileNameExcel) &&
                    _ExcelUtility.CheckHeader(file))
                {
                    using (ExcelPackage package = new ExcelPackage(file.OpenReadStream()))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        i = worksheet.Dimension.End.Row;
                    }
                    checkconfig = i <= 6001 && i > 1 ? true : false; //6000 rows + 1 for header
                }
                else if (Path.GetExtension(file.FileName).Equals(FaConstants.txt)
                    && file.FileName.Equals(_FileConfig.Value.FileNameTxT))
                {
                    using (StreamReader stream = new StreamReader(file.OpenReadStream()))
                    {
                        string ou = null;
                        while (stream.Peek() >= 0)
                        {
                            ou = stream.ReadLineAsync().Result.Trim();
                            i++;
                            if (ou.Length > 16 || ou.Contains(";"))
                            {
                                checkconfig = false;
                                return checkconfig;
                            }
                            else
                            {
                                resultTxT.Add(ou);
                            }
                        }
                        checkconfig = i <= 3000 && i > 0 ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error while checking file configuration: {ex.Message}");
            }
            return checkconfig;
        }


        public Tuple<bool, List<string>> CheckConfigurationv2(IFormFile file)
        {
            bool checkconfig = false;
            List<string> result = new List<string>();
            string textTxT = string.Empty;
            try
            {

                if (Path.GetExtension(file.FileName).Equals(FaConstants.txt)
                    && file.FileName.Equals(_FileConfig.Value.FileNameTxT))
                {
                    using (StreamReader stream = new StreamReader(file.OpenReadStream()))
                    {

                        textTxT = stream.ReadToEndAsync().Result;

                        if (!textTxT.Contains(";"))
                        {
                            return new Tuple<bool, List<string>>(checkconfig = false, result = null);
                        }
                        else
                        {

                            textTxT = textTxT.Replace(";", "\r\n");
                            string[] stringSeparators = new string[] { "\r\n" };
                            result = textTxT.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries).ToList<string>();

                            if (result.Count <= 3000)
                            {
                                checkconfig = true;
                                return new Tuple<bool, List<string>>(checkconfig, result);
                            }
                            else
                                return new Tuple<bool, List<string>>(checkconfig, result);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error while checking file configuration: {ex.Message}");
            }
            return new Tuple<bool, List<string>>(checkconfig, result);
        }
    }
}