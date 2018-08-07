using Microsoft.Extensions.Configuration;

namespace LaborAndStatistics.Application
{
    public static class ConfigurationExtensions
    {
        public static string DataFilesDirectory(this IConfiguration configuration)
        {
            return configuration["DataFiles"];
        }

        public static string TimeSeriesUrl(this IConfiguration configuration)
        {
            return configuration["TimeSeriesUrl"];
        }
    }
}