using System;
using System.Collections.Generic;
using rulesmngt.Interfaces;
using rulesmngt.Models;
using System.Linq;
using Microsoft.Extensions.Options;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Data
{
    public class CountryData : ICountryData
    {
        private readonly IOptions<DatabaseConfiguration> databaseConfiguration;

        public CountryData(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            this.databaseConfiguration = databaseConfiguration;
        }

        public void InsertCountry(Country country)
        {
            //TODO
        }

        public List<Country> GetCountries()
        {
            List<Country> countries;

            try
            {
                using (RulesContext context = new RulesContext(databaseConfiguration))
                {
                    countries = (from country in context.Country select country).ToList<Country>();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetCountries: " + e.Message);
            }

            return countries;
        }

        public Country GetCountryByNationAcronym(string nationAcronym)
        {
            Country country;
            
            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    country = (from c in context.Country 
                               where string.Equals(c.NationAcronym, nationAcronym,
                                                   StringComparison.OrdinalIgnoreCase) 
                               select c).SingleOrDefault<Country>();
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetCountryByNationAcronym: " + e.Message);
            }            

            return country;
        }

        public Country GetCountryByNation(string nation)
        {
            Country country;
            
            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    country = (from c in context.Country 
                               where string.Equals(c.Nation, nation,
                                                   StringComparison.OrdinalIgnoreCase) 
                               select c).SingleOrDefault<Country>();                               
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetCountryByNation: " + e.Message);
            }

            return country;
        }

        public Country GetCountryById(int countryId)
        {
            Country country;
        
            try
            {
                using (var context = new RulesContext(databaseConfiguration))
                {
                    country = (from c in context.Country 
                                where c.CountryId == countryId
                                select c).SingleOrDefault<Country>();                               
                }
            } 
            catch (Exception e)
            {
                throw new System.ArgumentException("Error in method GetCountryByNation: " + e.Message);
            }

            return country;
        }
    }
}