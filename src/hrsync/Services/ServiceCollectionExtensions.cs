using Microsoft.Extensions.DependencyInjection;

using hrsync.Data;
using hrsync.Helpers;
using hrsync.Interface;

namespace hrsync.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHrSyncAdapter, HrSyncAdapter>();
            services.AddTransient<IHrMasterdataOuData, HrMasterdataOuData>();
            services.AddTransient<IReportData, ReportData>();
            services.AddScoped<IClientAuthentication, ClientAuthenticationService>();
            services.AddScoped<IExtractionService, ExtractionService>();
            services.AddTransient<IFullSync, FullSync>();
            services.AddSingleton<IExtractionCustomService, ExtractionCustomService>();
            services.AddSingleton<IExtractionFullService, ExtractionFullService>();

            services.AddHostedService<BackgroundServiceTask>();
            services.AddSingleton<IHrMasterdataOuData, HrMasterdataOuData>();
            services.AddTransient<IHrSchedulerConfigurationData, HrSchedulerConfigurationData>();
            services.AddTransient<IMailSender, MailSender>();
            return services;
        }
    }
}
