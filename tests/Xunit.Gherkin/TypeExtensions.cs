using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Gherkin.Steps;

namespace Xunit.Gherkin
{
    public static class TypeExtensions
    {
        public static IEnumerable<MethodInfo> GetMethodsWithStepAttributes(this Type type)
        {
            return type.GetMethods()
                .Where(m => m.HasStepAttributes());
        }
    }
}