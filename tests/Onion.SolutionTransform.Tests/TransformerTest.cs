using NUnit.Framework;
using Onion.SolutionTransform.Parser;

namespace Onion.SolutionTransform.Tests
{
    [TestFixture]
    public class TransformerTest
    {
        [Test]
        public void Constructor_should_set_ParserInfo_property()
        {
            var slnPath = TestUtility.GetFixturePath(@"ndriven\NDriven.sln");
            var trans = new Transformer(slnPath);
            Assert.IsInstanceOf<IParserInfo>(trans.ParserInfo);
        }
    }
}