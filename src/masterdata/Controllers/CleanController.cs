using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    [Route("masterdata/v1/cleaning")]
    [ApiExplorerSettings(GroupName = "v1")]

    public class CleanController : ControllerBase
    {
        private readonly ILogger Logger;
        private readonly IOptions<Client> ClientData;
        private readonly IFAData _FaData;
        private readonly ICleanHrOU _ExcelData;
        private readonly ICheckFileConfiguration _CheckFileConfig;
        private readonly IExcelUtility _ExcelUtility;
        private readonly IFaManager _FaManager;

        public CleanController(ILoggerFactory loggerFactory,
                               IOptions<Client> clientdata,
                               ICleanHrOU exceldata,
                               IFAData faData,
                               ICheckFileConfiguration fileconfig,
                               IExcelUtility excelUtility,
                               IFaManager faManager)
        {
            this.Logger = loggerFactory.CreateLogger<CleanController>();
            this.ClientData = clientdata;
            this._ExcelData = exceldata;
            this._FaData = faData;
            this._CheckFileConfig = fileconfig;
            this._ExcelUtility = excelUtility;
            this._FaManager = faManager;
        }

        [HttpPost, DisableRequestSizeLimit]
        [ApiExplorerSettings(GroupName = "v1")]
        public ActionResult postFile()
        {
            List<CleanHrOU> resultExcel = new List<CleanHrOU>();
            List<string> resultTxT = new List<string>();
            FunctionalAck faData = null;

            try
            {
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    var file = HttpContext.Request.Form.Files;

                    bool isExcelFile = Path.GetExtension(file[0].FileName).Equals(FaConstants.xlsx);

                    faData = new FunctionalAck(ClientData.Value.Username, DateTime.Now, isExcelFile ? FaConstants.xlsx : FaConstants.txt, FaConstants.uploading);

                    if (_FaManager.CanStart(faData))
                    {
                        if (file != null && _CheckFileConfig.CheckConfiguration(file[0], out resultTxT))
                        {

                            faData = _FaData.AddNewFA(faData.caller, faData.issueTime, faData.inputType);
                            _FaManager.SetCurrFa(faData);

                            using (StreamReader stream = new StreamReader(file.FirstOrDefault().OpenReadStream()))
                            {
                                if (isExcelFile)
                                {
                                    resultExcel = _ExcelUtility.computeExcel(file[0]);
                                    _ExcelData.AddNewExcelData(resultExcel);
                                }
                                else
                                {
                                    _ExcelData.AddNewTxTData(resultTxT, faData.idFa);
                                }
                            }
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
        [ApiExplorerSettings(GroupName = "v1")]
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
                    return Ok(fa);
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

        [HttpGet("output/q")]
        [ApiExplorerSettings(GroupName = "v1")]
        public ActionResult<List<CleanBwCC>> GetCCS([FromQuery(Name = "fa_id")]string faid)
        {

            List<CleanBwCC> result = null;
            FunctionalAck fa = null;
            try
            {
                int faint;
                bool isInt = int.TryParse(faid, out faint);

                if (isInt)
                {
                    fa = _FaData.FaStatus(faid);

                    if (fa != null)
                    {
                        if (fa.status.Equals(FaConstants.uploading)
                         || fa.status.Equals(FaConstants.pending)
                         || fa.status.Equals(FaConstants.running))
                        {
                            return Ok("The elaboration for the requested FA is not over yet, try again later");
                        }
                        else if (fa.status.Equals(FaConstants.ended))
                        {
                            result = _FaData.GetCCS(faid);
                            return Ok(result);
                        }
                        else if (fa.status.Equals(FaConstants.Failed))
                        {
                            return Ok("The elaboration for the requested FA has failed, meaning no data is available.");
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
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
