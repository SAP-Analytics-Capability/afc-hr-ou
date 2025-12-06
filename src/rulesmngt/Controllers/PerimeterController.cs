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
    [Route("rulesmngt/v1/perimeter")]
    public class PerimeterController : ControllerBase
    {

        private IConfiguration Configuration;
        private ILogger logger;
        private IPerimeterData perimeterData;
        
        public PerimeterController(IConfiguration configuration, ILoggerFactory loggerFactory, IPerimeterData perimeterData)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<PerimeterController>();
            this.perimeterData = perimeterData;
        }


        [HttpGet]
        public ActionResult<List<Perimeter>> GetPerimeters()
        {
            List<Perimeter> perimeters;

            try
            {
                perimeters = perimeterData.GetPerimeters();
            }   
            catch (Exception e)
            {
                perimeters = null;
                this.logger.LogError(e, "An error occurred while excuting PerimeterController.GetPerimeters. Message: {0}.", e.Message);
            }

            return perimeters;
        }

       
        [HttpGet("getbyid/q")]
        public ActionResult<Perimeter> GetPerimeterById([FromQuery(Name = "id")]string perimeterId)
        {
            Perimeter perimeter;

            try
            {
                int perimeterIdInt = Int32.Parse(perimeterId);
                perimeter = perimeterData.GetPerimeterById(perimeterIdInt);
            }   
            catch (Exception e)
            {
                perimeter = null;
                this.logger.LogError(e, "An error occurred while excuting PerimeterController.GetPerimeterById. Message: {0}.", e.Message);
            }

            return perimeter;
        }

        [HttpGet("getbyname/q")]
        public ActionResult<Perimeter> GetPerimeterByName([FromQuery(Name = "name")]string perimeterName)
        {
            Perimeter perimeter;

            try
            {
                perimeter = perimeterData.GetPerimeterByName(perimeterName);
            }   
            catch (Exception e)
            {
                perimeter = null;
                this.logger.LogError(e, "An error occurred while excuting PerimeterController.GetPerimeterByName. Message: {0}.", e.Message);
            }

            return perimeter;
        }

        [HttpGet("getbydesc/q")]
        public ActionResult<Perimeter> GetPerimeterByDesc([FromQuery(Name = "desc")]string perimeterDesc)
        {
            Perimeter perimeter;

            try
            {
                perimeter = perimeterData.GetPerimeterByDesc(perimeterDesc);
            }   
            catch (Exception e)
            {
                perimeter = null;
                this.logger.LogError(e, "An error occurred while excuting PerimeterController.GetPerimeterByDesc. Message: {0}.", e.Message);
            }

            return perimeter;
        }
		
    }
}
