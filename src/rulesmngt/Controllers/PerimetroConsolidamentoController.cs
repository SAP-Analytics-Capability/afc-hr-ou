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
    [Route("rulesmngt/v1/pconsolidamento")]
    public class PerimetroConsolidamentoController : ControllerBase
    {
        private IConfiguration Configuration;
        private ILogger logger;
        private IPerimetroConsolidamentoData perimetroConsolidamentoData;

        public PerimetroConsolidamentoController(IConfiguration configuration, ILoggerFactory loggerFactory,
                                                IPerimetroConsolidamentoData perimetroConsolidamentoData)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<CountryController>();
            this.perimetroConsolidamentoData = perimetroConsolidamentoData;
        }

        [HttpGet("updateTable")]
        public ActionResult<String> updateTable()
        {

            try
            {

                return Ok("tabella perimetro di consolidamento aggiornata");
            }
            catch (Exception e)
            {

                this.logger.LogError(e, " Message: {0}.", e.Message);
                return StatusCode(500, string.Format("Message: {0}", e.Message));


            }

        }

        [HttpGet("q")]
        public IActionResult GetRecordsByAFCCompanies([FromQuery(Name = "afc_new_primo_descr")] string afcNewPrimoDescr)
        {
            List<PerimetroConsolidamento> pclist = new List<PerimetroConsolidamento>();
            try
            {
                pclist = perimetroConsolidamentoData.GetByNewPrimoDescr(afcNewPrimoDescr);
                logger.LogInformation("-----> Recuperati records n. "+pclist.Count+" da perimetro consolidamento");
                if(pclist.Count>0)
                return Ok(pclist);
                else
                {
                    return Ok("non esiste corrispondenza sul db");    
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
