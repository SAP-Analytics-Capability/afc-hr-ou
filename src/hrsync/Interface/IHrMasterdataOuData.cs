using System;
using System.Collections.Generic;
using hrsync.Models;

namespace hrsync.Interface
{
    public interface IHrMasterdataOuData
    {
        bool AddNewOrganizationalUnit(HrmasterdataOu ou);
        void AddNewOrganizationalUnit(HrmasterdataOu ou, HrmasterdataOu oldou);
        HrmasterdataOu GetOrganizationalUnit(string oucode);
        List<HrmasterdataOu> GetAllByDate(DateTime date);
        void DeleteAllHrMasterDataOus();
        bool RemoveOrganizationalUnit(HrmasterdataOu ou);
        bool AddIfOrganizationalUnitsByCode(List<HrmasterdataOu> oulist, DateTime initservicetime);
        List<HrmasterdataOu> GetByDate(DateTime inputdate);
    }
}