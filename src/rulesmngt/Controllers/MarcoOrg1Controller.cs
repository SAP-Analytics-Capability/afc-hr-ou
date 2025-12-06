using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using rulesmngt.Interfaces;
using rulesmngt.Models;

namespace rulesmngt.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Route("rulesmngt/v1/macroorg1")]
    public class Marcoorg1Controller : ControllerBase
    {
		
        private IConfiguration Configuration;
        private ILogger logger;
        private IMacroOrg1Data macroOrg1Data;
        
        public Marcoorg1Controller(IConfiguration configuration, ILoggerFactory loggerFactory, IMacroOrg1Data macroOrg1Data)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<Marcoorg1Controller>();
            this.macroOrg1Data = macroOrg1Data;
        }

        [HttpGet]
        public ActionResult<List<MacroOrg1>> GetMacroOrg1s()
        {
            List<MacroOrg1> macroOrg1List;

            try
            {
                macroOrg1List = macroOrg1Data.GetMacroOrg1s();     
            }
            catch (Exception e)
            {
                macroOrg1List = null;
                this.logger.LogError(e, "An error occurred while excuting MacroOrg1Controller.GetMacroOrg1s. Message: {0}.", e.Message);
            }
               
            return macroOrg1List;
        }
       
        [HttpGet("getbyid/q")]
        public ActionResult<MacroOrg1> GetMacroOrg1ById([FromQuery(Name = "id")]string macroOrg1Id)
        {
            MacroOrg1 macroOrg1;

            try
            {
                int id = Int32.Parse(macroOrg1Id);
                macroOrg1 = macroOrg1Data.GetMacroOrg1ById(id);
            }
            catch (Exception e)
            {
                macroOrg1 = null;
                this.logger.LogError(e, "An error occurred while excuting MacroOrg1Controller.GetMacroOrg1ById. Message: {0}.", e.Message);
            }

            return macroOrg1;
        }

        [HttpGet("getbydesc/q")]
        public ActionResult<MacroOrg1> GetMacroOrg1ByDesc([FromQuery(Name = "desc")]string macroOrg1Desc)
        {
            MacroOrg1 macroOrg1;

            try
            {
                macroOrg1 = macroOrg1Data.GetMacroOrg1ByDesc(macroOrg1Desc);
            }   
            catch (Exception e)
            {
                macroOrg1 = null;
                this.logger.LogError(e, "An error occurred while excuting MacroOrg1Controller.GetMacroOrg1ByDesc. Message: {0}.", e.Message);
            }

            return macroOrg1;
        }

        [HttpGet("getbycode/q")]
        public ActionResult<MacroOrg1> GetMacroOrg1ByCode([FromQuery(Name = "code")]string macroOrg1Code)
        {
            MacroOrg1 macroOrg1;

            try
            {
                macroOrg1 = macroOrg1Data.GetMacroOrg1ByCode(macroOrg1Code);
            }   
            catch (Exception e)
            {
                macroOrg1 = null;
                this.logger.LogError(e, "An error occurred while excuting MacroOrg1Controller.GetMacroOrg1ByCode. Message: {0}.", e.Message);
            }

            return macroOrg1;
        }
		
    }
}
