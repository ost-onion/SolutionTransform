using NUnit.Framework;
using Onion.SolutionTransform.Replacement;

namespace Onion.SolutionTransform.Tests.Replacement
{
    [TestFixture]
    public class PatternTest
    {
        private Pattern _pattern;

        [SetUp]
        public void SetUp()
        {
            _pattern = new Pattern("Client.Project.Core", "Gala.Apple.Core");
        }

        [Test]
        public void ReplaceInString_should_replace_pattern_in_string()
        {
            var str = "<AssemblyName>Client.Project.Core</AssemblyName>";
            var replaced = _pattern.ReplaceInString(str);
            Assert.AreEqual("<AssemblyName>Gala.Apple.Core</AssemblyName>", replaced);
        }
    }
}
