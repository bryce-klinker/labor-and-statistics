using System.IO;
using System.Threading.Tasks;
using LaborAndStatistics.Application;
using LaborAndStatistics.Application.Collection;
using LaborAndStatistics.Features.General;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using Xunit.Gherkin.Steps;

namespace LaborAndStatistics.Features.Steps
{
    public class CollectDataFilesSteps
    {
        private readonly IConfiguration _configuration;
        private readonly IDataCollector _collector;

        public CollectDataFilesSteps(ITestOutputHelper output, FeatureConfiguration configuration)
        {
            _configuration = configuration;
            _collector = ApplicationServices.CreateLaborAndStatisticsProvider(configuration)
                .GetService<IDataCollector>();
        }
        
        [When("I collect data files for analysis")]
        public async Task WhenICollectDataFilesForAnalysis()
        {
            await _collector.Collect();
        }

        [Then("I should have the overview text file")]
        public async Task ThenIShouldHaveTheOverviewTextFile()
        {
            var dataFilesDirectory = _configuration.DataFilesDirectory();
            Assert.True(File.Exists(Path.Combine(dataFilesDirectory, "overview.txt")));
        }
    }
}