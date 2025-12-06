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
    [Route("rulesmngt/v1/vcs")]
    public class VcsController : ControllerBase
    {
		
        private IConfiguration Configuration;
        private ILogger logger;
        private IVcsData vcsData;
        
        public VcsController(IConfiguration configuration, ILoggerFactory loggerFactory, IVcsData vcsData)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<VcsController>();
            this.vcsData = vcsData;
        }


        [HttpGet]
        public ActionResult<List<Vcs>> GetVcs()
        {
            List<Vcs> vcsList;
            
            try
            {
                vcsList = vcsData.GetVcs();
            }   
            catch (Exception e)
            {
                vcsList = null;
                this.logger.LogError(e, "An error occurred while excuting VcsController.GetVcs. Message: {0}.", e.Message);
            }

            return vcsList;
        }

       
        [HttpGet("getbyid/q")]
        public ActionResult<Vcs> GetVcsById([FromQuery(Name = "id")]string vcsId)
        {
            Vcs vcs;

            try
            {
                int vcsIdInt = Int32.Parse(vcsId);
                vcs = vcsData.GetVcsById(vcsIdInt);
            }   
            catch (Exception e)
            {
                vcs = null;
                this.logger.LogError(e, "An error occurred while excuting VcsController.GetVcsById. Message: {0}.", e.Message);
            }

            return vcs;
        }

        [HttpGet("getbycode/q")]
        public ActionResult<Vcs> GetVcsByCode([FromQuery(Name = "code")]string vcsCode)
        {
            Vcs vcs;

            try
            {
                vcs = vcsData.GetVcsByCode(vcsCode);
            }   
            catch (Exception e)
            {
                vcs = null;
                this.logger.LogError(e, "An error occurred while excuting VcsController.GetVcsByCode. Message: {0}.", e.Message);
            }

            return vcs;
        }

        [HttpGet("getbydesc/q")]
        public ActionResult<Vcs> GetVcsByDesc([FromQuery(Name = "desc")]string vcsDesc)
        {
            Vcs vcs;

            try
            {
                vcs = vcsData.GetVcsByDesc(vcsDesc);
            }   
            catch (Exception e)
            {
                vcs = null;
                this.logger.LogError(e, "An error occurred while excuting VcsController.GetVcsByDesc. Message: {0}.", e.Message);
            }

            return vcs;
        }
		
    }
}
