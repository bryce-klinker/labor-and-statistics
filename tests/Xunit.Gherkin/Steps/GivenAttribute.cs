using System;

namespace Xunit.Gherkin.Steps
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GivenAttribute : StepAttribute
    {
        public GivenAttribute(string regex)
            : base(regex)
        {
        }
    }
}