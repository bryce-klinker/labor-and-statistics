using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Xunit.Gherkin.Steps
{
    public class StepDefinition
    {
        public Type Type { get; }
        public MethodInfo Method { get; }
        public StepAttribute Attribute { get; }

        public StepDefinition(Type type, MethodInfo method, StepAttribute attribute)
        {
            Type = type;
            Method = method;
            Attribute = attribute;
        }

        public bool IsMatch<T>(string text)
        {
            return Attribute.GetType() == typeof(T)
                   && Attribute.IsMatch(text);
        }

        public async Task Execute(string text, ITestOutputHelper output, params object[] injectableServices)
        {
            var methodParameters = Method.GetParameters();
            var textParameters = Attribute.GetParameters(text);
            if (methodParameters.Length != textParameters.Length)
                throw new InvalidOperationException("Parameter count mismatch");

            var instance = CreateInstance(output, injectableServices);
            var convertedParameters = ConvertParameters(methodParameters, textParameters).ToArray();
            if (!Method.IsAsync())
                Method.Invoke(instance, convertedParameters);
            else
                await (Task) Method.Invoke(instance, convertedParameters);
        }

        private static IEnumerable<object> ConvertParameters(IReadOnlyList<ParameterInfo> parameters, IReadOnlyList<string> values)
        {
            for (var i = 0; i < parameters.Count; i++)
                yield return Convert.ChangeType(values[i], parameters[i].ParameterType);
        }

        private object CreateInstance(ITestOutputHelper output, params object[] injectableServices)
        {
            var constructor = GetConstructor(injectableServices);

            var arguments = new[] {output}.Concat(injectableServices).ToArray();
            return constructor != null
                ? constructor.Invoke(arguments)
                : Activator.CreateInstance(Type);
        }

        private ConstructorInfo GetConstructor(params object[] injectableServices)
        {
            var parameterTypes = injectableServices.Select(i => i.GetType()).ToArray();
            var testOutputConstructor = Type
                .GetConstructor(new[] {typeof(ITestOutputHelper)}.Concat(parameterTypes).ToArray());
            return testOutputConstructor;
        }
    }
}