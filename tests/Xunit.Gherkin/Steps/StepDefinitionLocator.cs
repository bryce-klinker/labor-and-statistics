using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Xunit.Gherkin.Steps
{
    public class StepDefinitionLocator
    {
        private static readonly Lazy<StepDefinitionLocator> Locator =
            new Lazy<StepDefinitionLocator>(() => new StepDefinitionLocator());

        private StepDefinition[] _steps;

        public static StepDefinitionLocator Instance => Locator.Value;

        public StepDefinition[] GetDefinitions()
        {
            if (_steps != null)
                return _steps;

            var definitions = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetPublicTypes())
                .SelectMany(t => t.GetMethodsWithStepAttributes())
                .SelectMany(GetStepDefinitions)
                .ToArray();
            return _steps = definitions;
        }

        private static IEnumerable<StepDefinition> GetStepDefinitions(MethodInfo method)
        {
            return method.GetStepAttributes()
                .Select(a => new StepDefinition(method.DeclaringType, method, a));
        }
    }
}