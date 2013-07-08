using System.IO;
using System.Linq;
using NUnit.Framework;
using Onion.SolutionParser.Parser.Model;
using Onion.SolutionTransform.Parser;

namespace Onion.SolutionTransform.Tests.Parser
{
    [TestFixture]
    public class ParserInfoTest
    {
        private string _solutionPath;
        private ParserInfo _info;

        [SetUp]
        public void SetUp()
        {
            _solutionPath = TestUtility.GetFixturePath(@"ndriven\NDriven.sln");
            _info = new ParserInfo(_solutionPath);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Constructor_should_throw_file_not_found_exception_if_solution_does_not_exist()
        {
            var sln = "/road/to/nowhere";
            var transform = new ParserInfo(sln);
        }

        [Test]
        public void Constructor_should_set_SolutionPath_if_solution_file_exists()
        {
            Assert.AreEqual(_solutionPath, _info.SolutionPath);
        }

        [Test]
        public void Constructor_should_set_BasePath_if_solution_file_exists()
        {
            Assert.AreEqual(TestUtility.GetFixturePath("ndriven"), _info.BasePath);
        }

        [Test]
        public void GetSolution_should_parse_solution_and_cache_result()
        {
            var sln = _info.GetSolution();
            Assert.IsInstanceOf<ISolution>(sln);
            Assert.AreSame(sln, _info.GetSolution());
        }

        [Test]
        public void GetProjects_should_parse_solution_and_fetch_projects_and_cache_result()
        {
            var projects = _info.GetProjects();
            Assert.True(projects.Any());
            Assert.AreSame(projects, _info.GetProjects());
        }
    }
}
