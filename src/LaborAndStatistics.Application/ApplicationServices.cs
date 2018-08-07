using System;
using LaborAndStatistics.Application.Collection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LaborAndStatistics.Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddLaborAndStatisticsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDataCollector, DataCollector>();
            services.AddSingleton(configuration);
            services.AddHttpClient();
            return services;
        }

        public static IServiceProvider CreateLaborAndStatisticsProvider(IConfiguration configuration, IServiceCollection services = null)
        {
            return (services ?? new ServiceCollection())
                .AddLaborAndStatisticsServices(configuration)
                .BuildServiceProvider();
        }
    }
}