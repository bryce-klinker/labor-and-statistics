using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace LaborAndStatistics.Features.General
{
    public class FeatureConfiguration : IConfiguration
    {
        private readonly IConfiguration _inner;

        public FeatureConfiguration()
        {
            _inner = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>("DataFiles", Path.Join(Directory.GetCurrentDirectory(), "data-files")),
                    new KeyValuePair<string, string>("TimeSeriesUrl", "https://download.bls.gov/pub/time.series"), 
                }).Build();
        }
        
        public IConfigurationSection GetSection(string key)
        {
            return _inner.GetSection(key);
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return _inner.GetChildren();
        }

        public IChangeToken GetReloadToken()
        {
            return _inner.GetReloadToken();
        }

        public string this[string key]
        {
            get => _inner[key];
            set => _inner[key] = value;
        }
    }
}