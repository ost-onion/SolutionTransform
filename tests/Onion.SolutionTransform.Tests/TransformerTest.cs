using Moq;
using NUnit.Framework;
using Onion.SolutionTransform.Parser;
using Onion.SolutionTransform.Strategy;

namespace Onion.SolutionTransform.Tests
{
    [TestFixture]
    public class TransformerTest
    {
        private Transformer _transformer;

        [SetUp]
        public void SetUp()
        {
            var slnPath = TestUtility.GetFixturePath(@"ndriven\NDriven.sln");
            _transformer = new Transformer(slnPath);
        }

        [Test]
        public void Constructor_should_set_ParserInfo_property()
        {
            Assert.IsInstanceOf<IParserInfo>(_transformer.ParserInfo);
        }

        [Test]
        public void AddStrategy_should_add_strategy_and_return_self()
        {
            var strat = new Mock<ISolutionTransformStrategy>();
            var self = _transformer.AddStrategy(strat.Object);
            Assert.AreSame(_transformer, self);
        }
    }
}