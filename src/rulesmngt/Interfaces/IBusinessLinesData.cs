using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IBusinessLinesData
    {
        void InsertBusinessLines(BusinessLines businessLines);

        List<BusinessLines> GetBusinessLines();

        BusinessLines GetBusinessLinesById(int businessLinesId);
        
        List<BusinessLines> GetBusinessLinesByName(string BlDescription);
    }
}