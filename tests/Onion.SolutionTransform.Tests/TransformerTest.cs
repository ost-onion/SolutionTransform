using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Onion.SolutionTransform.Tests
{
    [TestFixture]
    public class TransformerTest
    {
        private string _solutionPath;
        private Transformer _transformer;

        [SetUp]
        public void SetUp()
        {
            _solutionPath = TestUtility.GetFixturePath(@"ndriven\NDriven.sln");
            _transformer = new Transformer(_solutionPath);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Constructor_should_throw_file_not_found_exception_if_solution_does_not_exist()
        {
            var sln = "/road/to/nowhere";
            var transform = new Transformer(sln);
        }

        [Test]
        public void Constructor_should_set_SolutionPath_if_solution_file_exists()
        {
            Assert.AreEqual(_solutionPath, _transformer.SolutionPath);
        }

        [Test]
        public void Constructor_should_set_BasePath_if_solution_file_exists()
        {
            Assert.AreEqual(TestUtility.GetFixturePath("ndriven"), _transformer.BasePath);
        }

        [Test]
        public void GetProjects_should_parse_solution_and_fetch_projects_and_cache_result()
        {
            var projects = _transformer.GetProjects();
            Assert.True(projects.Any());
            Assert.AreSame(projects, _transformer.GetProjects());
        }
    }
}