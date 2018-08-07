using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Xunit.Gherkin
{
    public static class AssemblyExtensions
    {
        public static bool IsFrameworkAssembly(this Assembly assembly)
        {
            var assemblyName = assembly.GetName();
            return assemblyName.FullName.Contains("System")
                   || assemblyName.FullName.Contains("Microsoft")
                   || assemblyName.FullName.Contains("testhost");
        }

        public static IEnumerable<Type> GetPublicTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes().Where(t => t.IsPublic);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Type>();
            }
        }
    }
}