using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace masterdata
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseShutdownTimeout(TimeSpan.FromSeconds(10))
                .ConfigureLogging((webhostContext, builder) =>
                {
                    builder.AddConfiguration(webhostContext.Configuration.GetSection("Logging"))
                    .AddConsole()
                    .AddDebug();
                })
                //.UseUrls("http://*:5005")
                .UseStartup<Startup>();
    }
}
