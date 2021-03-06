﻿using System.Reflection;
using Xunit.Abstractions;

namespace Xunit.Gherkin
{
    public static class TestOutputHelperExtensions
    {
        public static ITest GetTest(this ITestOutputHelper output)
        {
            var type = output.GetType();
            var testMember = type.GetField("test", BindingFlags.Instance | BindingFlags.NonPublic);
            return (ITest)testMember.GetValue(output);
        } 
    }
}