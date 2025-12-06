using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using masterdata.Models;
using masterdata.Interfaces;

namespace masterdata.Controllers
{
    [Authorize]
    [Route("masterdata/v2/cleaning")]
    [ApiExplorerSettings(GroupName = "v2")]

    public class CleanControllerV2 : ControllerBase
    {
        private readonly ILogger Logger;
        private readonly IOptions<Client> ClientData;
        private readonly IFAData _FaData;
        private readonly ICleanHrOU _ExcelData;
        private readonly ICheckFileConfiguration _CheckFileConfig;
        private readonly IExcelUtility _ExcelUtility;
        private readonly IFaManager _FaManager;

        public CleanControllerV2(ILoggerFactory loggerFactory,
                               IOptions<Client> clientdata,
                               ICleanHrOU exceldata,
                               IFAData faData,
                               ICheckFileConfiguration fileconfig,
                               IExcelUtility excelUtility,
                               IFaManager faManager)
        {
            this.Logger = loggerFactory.CreateLogger<CleanControllerV2>();
            this.ClientData = clientdata;
            this._ExcelData = exceldata;
            this._FaData = faData;
            this._CheckFileConfig = fileconfig;
            this._ExcelUtility = excelUtility;
            this._FaManager = faManager;
        }

        [HttpPost, DisableRequestSizeLimit]
        [ApiExplorerSettings(GroupName = "v2")]
        public ActionResult postFile()
        {
            List<CleanHrOU> resultExcel = new List<CleanHrOU>();
            FunctionalAck faData = null;

            try
            {
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    var file = HttpContext.Request.Form.Files;

                    faData = new FunctionalAck(ClientData.Value.Username, DateTime.Now, FaConstants.txt, FaConstants.uploading);

                    if (_FaManager.CanStart(faData))
                    {
                        Tuple<bool, List<string>> tuple = _CheckFileConfig.CheckConfigurationv2(file[0]);
                        
                        if (tuple.Item1 && tuple.Item2.Count > 0 && tuple.Item2 !=null)
                        {

                            faData = _FaData.AddNewFA(faData.caller, faData.issueTime, faData.inputType);
                            _FaManager.SetCurrFa(faData);
                            _ExcelData.AddNewTxTData(tuple.Item2, faData.idFa);

                        }
                        else
                        {
                            _FaManager.SetCurrFa(new FunctionalAck());
                            return BadRequest("File configuration not valid");
                        }
                    }
                    else
                    {
                        return Conflict("There is a FA already running, cant accept any untill process ends.");
                    }
                }
                else
                {
                    return BadRequest("No file has been found");
                }

                _FaData.UpdateFaDbStatus(faData, "Pending");
                return Ok(faData);

            }
            catch (Exception ex)
            {
                _FaData.DeleteFA(faData);
                if (!_FaManager.GetCurrFA().status.Equals(FaConstants.running))
                {
                    _FaManager.SetCurrFa(new FunctionalAck());
                }
                Logger.LogError($"Error while uploading file: {ex.Message}");
                return BadRequest("There was an error while uploading the file, FA has been cancelled.");
            }
        }


        [HttpGet("q")]
        [ApiExplorerSettings(GroupName = "v2")]
        public ActionResult<FunctionalAck> FaStatus([FromQuery(Name = "fa_id")]string faid)
        {

            FunctionalAck fa = new FunctionalAck();
            try
            {
                int faint;
                bool isInt = int.TryParse(faid, out faint);

                if (isInt)
                {

                    fa = _FaData.FaStatus(faid);
                    return fa;
                }
                else
                {
                    return BadRequest($"Invalid FA id: {faid}");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error while checking FA status: {ex.Message}");
                return NoContent();
            }
        }
    }
}
