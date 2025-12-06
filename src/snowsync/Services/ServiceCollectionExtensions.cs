using Microsoft.Extensions.DependencyInjection;

using snowsync.Helpers.Adapters;
using snowsync.Interfaces;
using snowsync.Models;

namespace snowsync.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<ISnowAdatpter, SnowAdapter>();
            return services;
        }
    }
}