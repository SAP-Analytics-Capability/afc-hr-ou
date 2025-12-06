using System;
using System.Collections.Generic;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IMacroOrg1Data
    {
        void InsertMacroOrg1(MacroOrg1 macroOrg1);

        MacroOrg1 GetMacroOrg1ById(int MacroOrg1Code);

        MacroOrg1 GetMacroOrg1ByDesc(string MacroOrg1Desc);

        MacroOrg1 GetMacroOrg1ByCode(string macroOrg1Code);

        List<MacroOrg1> GetMacroOrg1s();

    }
}