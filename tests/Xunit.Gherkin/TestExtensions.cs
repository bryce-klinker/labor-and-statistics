using System.Reflection;
using Xunit.Abstractions;
using Xunit.Gherkin.Scenarios;

namespace Xunit.Gherkin
{
    public static class TestExtensions
    {
        public static string GetScenarioName(this ITest test)
        {
            var method = test.TestCase.TestMethod.Method;
            var scenarioAttribute = method.ToRuntimeMethod()
                .GetCustomAttribute<ScenarioAttribute>();
            return scenarioAttribute?.Name ?? "Unknown Scenario";
        }
    }
}