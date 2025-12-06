using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IPerimeterData
    {
        void InsertPerimeter(Perimeter perimetro);

        List<Perimeter> GetPerimeters();

        Perimeter GetPerimeterById(int perimiterId);

        Perimeter GetPerimeterByName(string perimiterName);

        Perimeter GetPerimeterByDesc(string perimeterDesc);
    }
}