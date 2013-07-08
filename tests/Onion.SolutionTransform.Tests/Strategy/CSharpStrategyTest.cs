using System.Linq;
using NUnit.Framework;
using Onion.SolutionTransform.Parser;
using Onion.SolutionTransform.Strategy;

namespace Onion.SolutionTransform.Tests.Strategy
{
    [TestFixture]
    public class CSharpStrategyTest
    {
        private IParserInfo _info;
        private CSharpStrategy _strategy;

        [SetUp]
        public void SetUp()
        {
            var slnPath = TestUtility.GetFixturePath(@"ndriven\NDriven.sln");
            _info = new ParserInfo(slnPath);
            _strategy = new CSharpStrategy {ParserInfo = _info};
        }

        [Test]
        public async void Transform_should_change_using_and_namespace_statements()
        {
            _info.GetProjects().First(p => p.Name == "Core").Name = "AppleCore";
            await _strategy.TransformAsync();
            var iEntitySrc = TestUtility.GetFileContents(@"ndriven\src\Core\Domain\Model\IEntity.cs");
            var expectedIEntity = TestUtility.GetFileContents("RenamedNamespace.txt");
            var authServiceSrc = TestUtility.GetFileContents(@"ndriven\src\Presentation.Web\Services\AuthenticationService.cs");
            var expectedAuthService = TestUtility.GetFileContents("RenamedUsing.txt");
            Assert.AreEqual(expectedIEntity, iEntitySrc);
            Assert.AreEqual(expectedAuthService, authServiceSrc);
        }
    }
}
