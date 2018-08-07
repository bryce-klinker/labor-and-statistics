using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Gherkin.Steps;

namespace Xunit.Gherkin.Features
{
    public class Feature
    {
        public bool HasPrintedScenario { get; private set; }
        public StepDefinition[] Steps { get; }
        public ITestOutputHelper Output { get; }
        public object[] Services { get; }
        public ITest Test { get; }
        public string ScenarioName { get; }

        public Feature(ITestOutputHelper output, params object[] services)
        {
            Output = output;
            Services = services;
            Test = output.GetTest();
            ScenarioName = Test.GetScenarioName();
            Steps = StepDefinitionLocator.Instance.GetDefinitions();
        }

        public async Task Given(string text)
        {
            await ExecuteStep<GivenAttribute>(text);
        }

        public async Task When(string text)
        {
            await ExecuteStep<WhenAttribute>(text);
        }

        public async Task Then(string text)
        {
            await ExecuteStep<ThenAttribute>(text);
        }
        
        private async Task ExecuteStep<T>(string text)
        {
            var step = Steps.SingleOrDefault(s => s.IsMatch<T>(text))
                       ?? throw new InvalidOperationException($"No matching step definition was found for: {text}");

            if (!HasPrintedScenario)
                OutputScenario();

            var prefix = step.Attribute.GetType().Name.Replace("Attribute", "");
            Output.WriteLine($"{prefix} {text}");
            await step.Execute(text, Output, Services);
        }

        private void OutputScenario()
        {
            HasPrintedScenario = true;
            Output.WriteLine($"Scenario: {ScenarioName}");
        }
    }
}