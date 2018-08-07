using System.Threading.Tasks;
using LaborAndStatistics.Features.General;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;
using Xunit.Gherkin.Features;
using Xunit.Gherkin.Scenarios;

namespace LaborAndStatistics.Features
{
    public class CollectDataFilesFeature : Feature
    {
        public CollectDataFilesFeature(ITestOutputHelper output) 
            : base(output, new FeatureConfiguration() as IConfiguration)
        {
        }

        [Fact]
        [Scenario("Download Overview File")]
        public async Task ShouldDownloadOverviewFile()
        {
            await When("I collect data files for analysis");
            await Then("I should have the overview text file");
        }
    }
}