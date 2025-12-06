using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IAreaData
    {
        void InsertArea(Area area);

        List<Area> GetAreas();

        Area GetAreaById(int areaId);

        Area GetAreaByCode(string areaCode);
    }
}