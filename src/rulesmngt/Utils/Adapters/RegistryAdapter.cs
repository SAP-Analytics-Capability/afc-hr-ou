using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using rulesmngt.Models;
using Newtonsoft.Json;


namespace rulesmngt.Utils.Adapters
{
    public class RegistryAdapter
    {

        private static RegistryAdapter _instance;

        private RegistryAdapter() { }

        public static RegistryAdapter getInstance()
        {
            if (_instance == null)
                _instance = new RegistryAdapter();

            return _instance;
        }
   

        //   public async Task<PuInfo> getPuInfoIdPmax(Microsoft.Extensions.Configuration.IConfiguration config, CancellationToken cancellationToken, int puId)
        // {   
        //   /*   string baseUrl = config["Endpoint:Registry:Base"] + "/" + config["Endpoint:Registry:ServiceName"] + "/" + config["Endpoint:Registry:Version"] 
        //   +"/"  + config["Endpoint:Registry:ProdUnit"] + "inputhr/" + puId; */
        //   string baseUrl = "http://" + config["REGISTRY_SERVICE_HOST"] + ":" + config["REGISTRY_SERVICE_PORT"]  + "/" + config["Endpoint:Registry:ServiceName"] + "/" + config["Endpoint:Registry:Version"] 
        //   +"/"  + config["Endpoint:Registry:ProdUnit"] + "inputhr/" + puId; 
        //     PuInfo puInfo = new PuInfo();

        //     cancellationToken.ThrowIfCancellationRequested();

        //     //Create a new instance of HttpClient
        //     using (HttpClient client = new HttpClient())
        //     {
        //         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //         using (HttpResponseMessage res = await client.GetAsync(baseUrl, cancellationToken))
        //         {
        //             if (res.IsSuccessStatusCode)
        //             {
        //                 using (HttpContent content = res.Content)
        //                 {
        //                     string json = await content.ReadAsStringAsync();
        //                     puInfo = JsonConvert.DeserializeObject<PuInfo>(json);
        //                 }
        //             }
        //         }
        //     }
        //     return puInfo;
        // }
        
    }
} 