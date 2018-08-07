namespace Xunit.Gherkin.Steps
{
    public class WhenAttribute : StepAttribute
    {
        public WhenAttribute(string regex) : base(regex)
        {
        }
    }
}