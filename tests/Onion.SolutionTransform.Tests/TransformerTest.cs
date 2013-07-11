using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Onion.SolutionTransform.Parser;
using Onion.SolutionTransform.Strategy;
using Onion.SolutionTransform.Template;

namespace Onion.SolutionTransform.Tests
{
    [TestFixture]
    public class TransformerTest
    {
        private Transformer _transformer;

        [SetUp]
        public void SetUp()
        {
            var slnPath = TestUtility.GetFixturePath(@"transform\ndriven\NDriven.sln");
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
            Assert.AreEqual(1, _transformer.Strategies.Count);
        }

        [Test]
        public void AddStrategy_should_set_ParserInfo_if_null()
        {
            var strat = new ProjectStrategy();
            _transformer.AddStrategy(strat);
            Assert.AreSame(_transformer.ParserInfo, strat.ParserInfo);
        }

        [Test]
        public void AddStrategy_should_not_set_ParserInfo_if_already_set()
        {
            var strat = new ProjectStrategy();
            var info = new Mock<IParserInfo>();
            strat.ParserInfo = info.Object;

            _transformer.AddStrategy(strat);
            Assert.AreNotSame(_transformer.ParserInfo, strat.ParserInfo);
        }

        [Test]
        public void AddTemplate_should_add_any_strategies_included_and_return_self()
        {
            var self = _transformer.AddTemplate(new TestTemplate());
            Assert.AreSame(_transformer, self);
            Assert.AreEqual(2, _transformer.Strategies.Count);
        }
    }

    public class TestTemplate : ISolutionTransformTemplate
    {
        public IEnumerable<ISolutionTransformStrategy> GetStrategies()
        {
            var strat1 = new Mock<ISolutionTransformStrategy>().Object;
            var strat2 = new Mock<ISolutionTransformStrategy>().Object;
            return new List<ISolutionTransformStrategy>()
                {
                    strat1,
                    strat2
                };
        }
    }
}