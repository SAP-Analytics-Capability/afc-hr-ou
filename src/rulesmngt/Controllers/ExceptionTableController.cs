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
    [Route("rulesmngt/v1/exceptiontable")]
    public class ExceptionTableController : ControllerBase
    {
		
        private IConfiguration Configuration;
        private ILogger logger;
        private IExceptionTableData ExceptionTableData;
        
        public ExceptionTableController(IConfiguration configuration, ILoggerFactory loggerFactory, IExceptionTableData ExceptionTableData)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<IExceptionTableData>();
            this.ExceptionTableData = ExceptionTableData;
        }

        [HttpGet]
        public ActionResult<List<ExceptionTable>> GetExceptions()
        {
            List<ExceptionTable> exceptions;

            try
            {
                exceptions = ExceptionTableData.GetExceptions();
            }
            catch (Exception e)
            {
                exceptions = null;
                this.logger.LogError(e, "An error occurred while excuting ExceptionTableController.GetExceptions. Message: {0}.", e.Message);
            }
                   
            return exceptions;
        }

       
        [HttpGet("getbytypouo/q")]
        public ActionResult<ExceptionTable> GetExceptionTableTypo([FromQuery(Name = "typouo")]string TypoUo)
        {
            ExceptionTable exception;

            try
            {
                exception = ExceptionTableData.GetExceptionByTypoUo(TypoUo);
            }   
            catch (Exception e)
            {
                exception = null;
                this.logger.LogError(e, "An error occurred while excuting ExceptionTableController.GetExceptions. Message: {0}.", e.Message);
            } 
            
            return exception;
        }

        [HttpGet("getbytypouogblprev/q")]
        public ActionResult<List<ExceptionTable>> GetExceptionByTypoUoGblPrev([FromQuery(Name = "typouo")]string TypoUo , [FromQuery(Name = "gblprev")]string GblPrev)
        {
            List<ExceptionTable> exceptions;

            try
            {
                exceptions = ExceptionTableData.GetExceptionByTypoUoGblPrev(TypoUo, GblPrev);
            }   
            catch (Exception e)
            {
                exceptions = null;
                this.logger.LogError(e, "An error occurred while excuting ExceptionTableController.GetExceptions. Message: {0}.", e.Message);
            } 
            
            return exceptions;
        }
        
    }
}
