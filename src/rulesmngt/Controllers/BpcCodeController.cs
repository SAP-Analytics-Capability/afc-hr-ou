using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using rulesmngt.Data;
using rulesmngt.Models;
using rulesmngt.Interfaces;


namespace rulesmngt.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Route("rulesmngt/v1/bpc")]
    public class BpcController : ControllerBase
    {
		
        private IConfiguration Configuration;
        private ILogger logger;
        private IBpcData bpcCodeData;


        public BpcController(IConfiguration configuration, ILoggerFactory loggerFactory, IBpcData bpcCodeData)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<ActivityListController>();
            this.bpcCodeData = bpcCodeData;
        }
        
        [HttpGet]
        public ActionResult<List<Bpc>> GetBpcCodes()
        {
            List<Bpc> areaList = bpcCodeData.GetBpc();        
            return areaList;
        }

       
        [HttpGet("getbyid/q")]
        public ActionResult<Bpc> GetBpcCodeById([FromQuery(Name = "id")]string bpcId)
        {
            Bpc bpcCode;

            try
            {
                int bpcIdInt = Int32.Parse(bpcId);
                bpcCode = bpcCodeData.GetBpcById(bpcIdInt);
            }   
            catch (Exception e)
            {
                bpcCode = null;
                this.logger.LogError(e, "An error occurred while excuting BpcController.GetBpcCodeById. Message: {0}.", e.Message);
            } 

            return bpcCode;
        }
         
        [HttpGet("getbydesc/q")]
        public ActionResult<Bpc> GetBpcCodeByDesc([FromQuery(Name = "desc")]string bpcDesc)
        {
            Bpc bpcCode;

            try
            {
                bpcCode = bpcCodeData.GetBpcByDesc(bpcDesc);
            }   
            catch (Exception e)
            {
                bpcCode = null;
                this.logger.LogError(e, "An error occurred while excuting BpcController.GetBpcByDesc. Message: {0}.", e.Message);
            } 

            return bpcCode;
        }
		
    }
}
