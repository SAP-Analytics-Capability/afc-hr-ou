using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using bwsync.Interfaces;
using bwsync.Helpers.Adapters;
using bwsync.Helpers.Utils;
using bwsync.Models;

namespace bwsync.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<Client>();
            services.AddSingleton<IBwSyncDataAdapter, BwSyncDataAdapter>();
            services.AddSingleton<IBwSapDataAdapter, BwSapDataAdapter>();
            services.AddScoped<IClientAuthentication, ClientAuthenticationService>();
            services.AddTransient<IMailSender, MailSender>();
            return services;
        }
    }
}