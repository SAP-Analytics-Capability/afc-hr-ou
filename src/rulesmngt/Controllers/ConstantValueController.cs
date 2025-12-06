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
    [Route("rulesmngt/v1/constantvalue")]
    public class ConstantValueController : ControllerBase
    {
		
        private IConfiguration Configuration;
        private ILogger logger;
        private IConstantValueData ConstantValueData;
        
        public ConstantValueController(IConfiguration configuration, ILoggerFactory loggerFactory, IConstantValueData ConstantValueData)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<IExceptionTableData>();
            this.ConstantValueData = ConstantValueData;
        }

        [HttpGet]
        public ActionResult<ConstantValue> GetConstant()
        {
            ConstantValue constant = new ConstantValue();
            try
            {
                constant = ConstantValueData.GetConstant();
            }
            catch (Exception e)
            {
                constant = null;
                this.logger.LogError(e, "An error occurred while excuting ExceptionTableController.GetConstants. Message: {0}.", e.Message);
            }
                   
            return constant;
        }
    }
}
