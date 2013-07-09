using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using Onion.SolutionTransform.Parser;
using Onion.SolutionTransform.Strategy;

namespace Onion.SolutionTransform.Tests.Strategy
{
    [TestFixture]
    public class ProjectStrategyTest
    {
        private IParserInfo _info;
        private ProjectStrategy _strategy;

        [SetUp]
        public void SetUp()
        {
            var slnPath = TestUtility.GetFixturePath(@"ndriven\NDriven.sln");
            _info = new ParserInfo(slnPath);
            _strategy = new ProjectStrategy { ParserInfo = _info };
        }

        [Test]
        public void Transform_should_change_AssemblyName_and_RootNamespace_nodes()
        {
            _info.GetProjects().First(p => p.Name == "Infrastructure.IoC").Name = "Inf.DI";
            _strategy.Transform();
            var projectSrc = TestUtility.GetFixturePath(@"ndriven\src\Infrastructure.IoC\Infrastructure.IoC.csproj");
            var expectedProjectXml = TestUtility.GetFixturePath("RenamedProjectFileContents.xml");
            var projectDoc = XDocument.Load(projectSrc);
            var expectedDoc = XDocument.Load(expectedProjectXml);

            Assert.True(XNode.DeepEquals(expectedDoc, projectDoc));
        }
    }
}
