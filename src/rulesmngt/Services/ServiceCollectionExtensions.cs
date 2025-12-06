using Microsoft.Extensions.DependencyInjection;

using rulesmngt.Data;
using rulesmngt.Interfaces;
using rulesmngt.Helpers.Utils;
using rulesmngt.Interfaces.Adapters;
using rulesmngt.Helpers.Adapters;
using masterdata.Data;
using rulesmngt.Helpers;
using rulesmngt.Utils;

namespace rulesmngt.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IActivityAssociationData, ActivityAssociationData>();
            services.AddTransient<ICompanyScopeData, CompanyScopeData>();
            services.AddTransient<IActivityListData, ActivityListData>();
            services.AddTransient<IActivityMappingData, ActivityMappingData>();
            services.AddTransient<IAreaData, AreaData>();
            services.AddTransient<IBpcData, BpcCodeData>();
            services.AddTransient<IBusinessLinesData, BusinessLinesData>();
            services.AddTransient<ICompanyRulesData, CompanyRulesData>();
            services.AddTransient<ICountryData, CountryData>();
            services.AddTransient<IEntityData, EntityData>();
            services.AddTransient<IMacroOrg1Data, MacroOrg1Data>();
            services.AddTransient<IOrganizationData, OrganizationData>();
            services.AddTransient<IPerimeterData, PerimeterData>();
            services.AddTransient<IProcessGlobalData, ProcessGlobalData>();
            services.AddTransient<IProcessLocalData, ProcessLocalData>();
            services.AddTransient<IVcsData, VcsData>();
            services.AddTransient<IPerimetroConsolidamentoData, PerimetroConsolidamentoData>();
            services.AddTransient<IItaGloData, ItaGloData>();
            services.AddTransient<IResponsability, ResponsabilityData>();
            services.AddTransient<IMailSender, MailSender>();
            services.AddTransient<IExceptionTableData, ExceptionTableData>();
            services.AddTransient<IConstantValueData, ConstantValueData>();
            services.AddTransient<ISnowAdatpter, SnowAdapter>();
            services.AddTransient<ICatalogSync, CatalogSync>();
            services.AddScoped<IClientAuthentication, ClientAuthenticationService>();
            services.AddTransient<ISchedulerConfigurationData, SchedulerConfigurationData>();
            services.AddTransient<IHrSyncAdapter, HrSyncAdapter>();
            services.AddTransient<IFullSync, FullSync>();

            services.AddHostedService<BackgroundServiceSnowTask>();

            services.AddSingleton<IClientAuthentication, ClientAuthenticationService>();
            services.AddSingleton<ICatalogUtils, CatalogUtils>();
            services.AddSingleton<IEventLogUtils, EventLogUtils>();

            return services;
        }
    }
}