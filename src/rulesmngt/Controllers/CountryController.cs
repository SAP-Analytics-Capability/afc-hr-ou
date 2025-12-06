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
    [Route("rulesmngt/v1/country")]
    public class CountryController : ControllerBase
    {
		
        private IConfiguration Configuration;
        private ILogger logger;
        private ICountryData countryData;
        
        public CountryController(IConfiguration configuration, ILoggerFactory loggerFactory, ICountryData countryData)
        {
            this.Configuration = configuration;
            this.logger = loggerFactory.CreateLogger<CountryController>();
            this.countryData = countryData;
        }

        [HttpGet]
        public ActionResult<List<Country>> GetCountries()
        {
            List<Country> countries;

            try
            {
                countries = countryData.GetCountries(); 
            }
            catch (Exception e)
            {
                countries = null;
                this.logger.LogError(e, "An error occurred while excuting CountryController.GetCountries. Message: {0}.", e.Message);
            }    

            return countries;
        }
       
        [HttpGet("getbynation/q")]
        public ActionResult<Country> GetCountryByNation([FromQuery(Name = "nation")]string nation)
        {
            Country country;
            
            try
            {
                country = countryData.GetCountryByNation(nation);
            }   
            catch (Exception e)
            {
                country = null;
                this.logger.LogError(e, "An error occurred while excuting CountryController.GetCountryByNation. Message: {0}.", e.Message);
            } 

            return country;
        }
         
        [HttpGet("getbynationacronym/q")]
        public ActionResult<Country> GetCountryByNationAcronym([FromQuery(Name = "nationacronym")]string nationAcronym)
        {
            Country country;
            
            try
            {
                country = countryData.GetCountryByNationAcronym(nationAcronym);
            }   
            catch (Exception e)
            {
                country = null;
                this.logger.LogError(e, "An error occurred while excuting CountryController.GetCountryByNationAcronym. Message: {0}.", e.Message);
            } 

            return country;
        }
		
    }
}
