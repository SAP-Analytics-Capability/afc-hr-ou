using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.ResponseCompression;

using masterdata.Helpers;
using masterdata.Helpers.Adapters;
using masterdata.Interfaces;
using masterdata.Interfaces.Adapters;
using masterdata.Services;
using masterdata.Models.Configuration;
using masterdata.Models;

namespace masterdata
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql()
              .AddDbContext<MasterDataContext>()
              .BuildServiceProvider();
            services.Configure<SnowAdapterConfiguration>(Configuration.GetSection("DataSource:SnowAdapterConfiguration"));
            services.Configure<BwSyncConfiguration>(Configuration.GetSection("DataSource:BwSyncConfiguration"));
            services.Configure<HrSyncConfiguration>(Configuration.GetSection("DataSource:HrSyncConfiguration"));
            services.Configure<RulesmngtConfigurations>(Configuration.GetSection("DataSource:RulesMngtConfiguration"));
            services.Configure<ProxyData>(Configuration.GetSection("DataSource:Proxy"));
            services.Configure<SchedulerOptions>(Configuration.GetSection("DataSource:SchedulerOptions"));
            services.Configure<EmailOptions>(Configuration.GetSection("DataSource:MailOptions"));
            services.Configure<DatabaseConfiguration>(Configuration.GetSection("DataSource:DatabaseConfiguration"));
            services.Configure<FileConfiguration>(Configuration.GetSection("DataSource:FileConfiguration"));

            services.RegisterServices();
            services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddHealthChecks();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;

            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Master Data API V1",
                    Description = "The Master Data API V1 collects data from HR Global Sync and BW Sync and it computes the association between the cost center and the organizational unit.",
                    TermsOfService = "https://corporate.enel.it/"
                });
                c.SwaggerDoc("v2", new Info
                {
                    Version = "v2",
                    Title = "Master Data API V2",
                    Description = "The Master Data API V2 collects data from HR Global Sync and BW Sync and it computes the association between the cost center and the organizational unit.",
                    TermsOfService = "https://corporate.enel.it/"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });



            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetValue<string>("DataSource:RedisConfiguration:ConnectionString");
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseResponseCompression();
            app.UseMvc();
            app.UseHealthChecks("/health");

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "V2 Docs");
            });
        }
    }
}
