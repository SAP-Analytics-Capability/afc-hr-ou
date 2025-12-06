using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface ICountryData
    {
        void InsertCountry(Country country);

        List<Country> GetCountries();

        Country GetCountryById(int country);

        Country GetCountryByNationAcronym(string nationAcronym);
        
        Country GetCountryByNation(string nation);
    }
}