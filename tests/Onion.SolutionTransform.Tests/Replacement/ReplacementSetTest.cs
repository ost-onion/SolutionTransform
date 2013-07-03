using System.Linq;
using NUnit.Framework;
using Onion.SolutionTransform.Replacement;

namespace Onion.SolutionTransform.Tests
{
    [TestFixture]
    public class ReplacementSetTest
    {
        private ReplacementSet _set;
        private Transformer _transformer;

        [SetUp]
        public void SetUp()
        {
            _transformer = new Transformer(TestUtility.GetFixturePath(@"ndriven\NDriven.sln"));
            _set = new ReplacementSet(_transformer);
        }

        [Test]
        public void Constructor_creates_patterns_with_word_boundaries_for_Projects_with_changed_names()
        {
            _transformer.GetProjects().First(p => p.Name == "Core").Name = "Client.Core";
            _transformer.GetProjects().First(p => p.Name == "Infrastructure.NHibernate").Name = "Client.Infrastructure.NHibernate";
            var set = new ReplacementSet(_transformer);
            Assert.AreEqual(2, set.Count);
        }
    }
}
