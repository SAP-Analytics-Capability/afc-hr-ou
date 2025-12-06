using masterdata.Models;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace masterdata.Interfaces
{
    public interface ICheckFileConfiguration
    {
        bool CheckConfiguration(IFormFile file, out List<string> resultTxT);
        Tuple<bool, List<string>> CheckConfigurationv2(IFormFile file);
    }
}