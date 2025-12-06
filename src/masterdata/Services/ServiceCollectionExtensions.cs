using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using masterdata.Helpers;
using masterdata.Helpers.Adapters;
using masterdata.Interfaces;
using masterdata.Interfaces.Adapters;
using masterdata.Models;
using masterdata.Data;
using masterdata.Models.Configuration;

namespace masterdata.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<Client>();
            services.AddTransient<ISnowAdatpter, SnowAdapter>();
            services.AddTransient<IHrSyncAdapter, HrSyncAdapter>();
            services.AddTransient<IBwSyncAdapter, BwSyncAdapter>();
            services.AddSingleton<IEntityAdapter, EntityAdapter>();
            services.AddSingleton<ICompanyRulesAdapter, CompanyRulesAdapter>();
            services.AddSingleton<IActivityMappingAdapter, ActivityMappingAdapter>();
            services.AddSingleton<IOuCCAssociation, OuCCAssociation>();
            services.AddSingleton<IFAData, FAData>();
            services.AddSingleton<ICleanHrOU, CleanHrOUData>();
            services.AddSingleton<IExcelUtility, ExcelUtility>();
            services.AddSingleton<IHostedService, BackgroundServiceCleanTask>();
            services.AddSingleton<IFaManager, FaManager>();
            services.AddSingleton<ICheckFileConfiguration, CheckFileConfiguration>();
            services.AddSingleton<IActivityAssociationAdapter, ActivityAssociationAdapter>();
            services.AddSingleton<ICompanyScopeAdapter, CompanyScopeAdapter>();
            services.AddSingleton<IExceptionTableAdapter, ExceptionTableAdapter>();
            services.AddSingleton<ICatalogManagementAdapter, CatalogManagementAdapter>();
            services.AddSingleton<IConstantValueAdapter, ConstantValueAdapter>();
            services.AddSingleton<ITypologyTranscodingData, TypologyTranscodingData>();


            // services.AddSingleton<IHostedService, BackgroundServiceTask>(); versione precedente MasterTask 18.12.19
            services.AddScoped<IClientAuthentication, ClientAuthenticationService>();
            services.AddTransient<IResultData, ResultData>();
            services.AddSingleton<IResultAdapter, ResultAdapter>();
            services.AddTransient<IHrMasterdataOuData, HrMasterdataOuData>();
            services.AddTransient<IClientAccessData, ClientAccessData>();
            services.AddTransient<IResponsabilityAdapter, ResponsabilityAdapter>();
            services.AddTransient<IItaGloAdapter, ItaGloAdapter>();
            services.AddTransient<ISequenceData, SequenceData>();
            services.AddTransient<IHrOuData, HrOuData>();
            services.AddTransient<IAssociationData, AssociationData>();
            services.AddTransient<ISchedulerConfigurationData, SchedulerConfigurationData>();
            services.AddScoped<IElaborationService, ElaborationService>();
            services.AddTransient<IDBCleaning, DBCleaning>();
            services.AddTransient<IMailSender, MailSender>();

            services.AddHostedService<BackgroundServiceSnowTask>();
            services.AddHostedService<BackgroundServiceMasterTask>();

            services.AddHttpClient();

            return services;
        }
    }
}