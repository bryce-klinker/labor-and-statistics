using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LaborAndStatistics.Application.Collection
{
    public interface IDataCollector
    {
        Task Collect();
    }

    public class DataCollector : IDataCollector
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public DataCollector(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task Collect()
        {
            if (!Directory.Exists(_configuration.DataFilesDirectory()))
                Directory.CreateDirectory(_configuration.DataFilesDirectory());
            
            using (var client = _httpClientFactory.CreateClient())
            {
                var contents = await client.GetStringAsync($"{_configuration.TimeSeriesUrl()}/overview.txt");
                var filePath = Path.Combine(_configuration.DataFilesDirectory(), "overview.txt");
                File.WriteAllText(filePath, contents);
            }
        }
    }
}