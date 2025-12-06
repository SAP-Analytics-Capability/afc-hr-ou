using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using hrsync.Interface;
using hrsync.Models;
using hrsync.Models.Configuration;
using System.Text;
using hrsync.Helpers;
using hrsync.Data;
using System.Linq;

namespace hrsync.Interface
{
    public interface IExtractionFullService
    {
        void MakeUOFullExtraction(List<HrmasterdataOuList> hrMasterdataOuList, HrmasterdataOuList synclist, DateTime serviceTime);

    }
}
