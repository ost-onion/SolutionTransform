using System.Linq;
using NUnit.Framework;
using Onion.SolutionTransform.Parser;
using Onion.SolutionTransform.Solution;

namespace Onion.SolutionTransform.Tests.Solution
{
    public class SolutionAssemblerTest
    {
        private ParserInfo _info;
        private SolutionAssembler _assembler;

        [SetUp]
        public void SetUp()
        {
            var path = TestUtility.GetFixturePath("NDriven.sln");
            _info = new ParserInfo(path);
            _assembler = new SolutionAssembler(_info);
            var projects = _info.GetProjects();
            projects.First(p => p.Name == "Core").Name = "AppleCore";
            projects.First(p => p.Name == "Test.Unit").Name = "Testing.Of.Units";
            projects.First(p => p.Name == "Presentation.Web").Name = "Presentation.Internet";
            projects.First(p => p.Name == "Infrastructure.NHibernate").Name = "Client.Infrastructure.ORM";
            projects.First(p => p.Name == "Infrastructure.Migrations").Name = "DB.Migrations";
        }

        [Test]
        public void GetAssembledSolution_should_return_solution_file_as_string()
        {
            var formatVersion = "12.00";
            var visStudioVersion = "2012";
            var sln = _assembler.GetAssembledSolution(formatVersion, visStudioVersion);
            var fixture = TestUtility.GetFileContents("ExpectedAssembly.sln");
            Assert.AreEqual(fixture, sln);
        }
    }
}
