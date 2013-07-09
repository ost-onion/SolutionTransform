using System.Linq;
using NUnit.Framework;
using Onion.SolutionTransform.Parser;
using Onion.SolutionTransform.Project;

namespace Onion.SolutionTransform.Tests.Project
{
    [TestFixture]
    public class ProjectDocumentTest
    {
        private ProjectDocument _doc;

        [SetUp]
        public void SetUp()
        {
            var solutionPath = TestUtility.GetFixturePath(@"ndriven\NDriven.sln");
            var info = new ParserInfo(solutionPath);
            var web = info.GetProjects().First(p => p.Name == "Presentation.Web");
            _doc = new ProjectDocument(web, info);
        }

        [Test]
        public void ProjectReferences_iterator_should_contain_project_ref_objects()
        {
            Assert.True(_doc.ProjectReferences.Any());
        }
    }
}
