using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Onion.SolutionTransform.Parser;
using Onion.SolutionTransform.Project;
using Onion.SolutionTransform.Solution;

namespace Onion.SolutionTransform.Tests.Solution
{
    public class SolutionAssemblerTest
    {
        const string FormatVersion = "12.00";
        const string VisualStudioVersion = "2012";
        private ParserInfo _info;
        private SolutionAssembler _assembler;

        [SetUp]
        public void SetUp()
        {
            var path = TestUtility.GetFixturePath("NDriven.sln");
            _info = new ParserInfo(path);
            _assembler = new SolutionAssembler(_info);
            var projects = _info.GetProjects();
            SetUpFiles();
            SetUpProjects(projects);
        }

        private static void SetUpProjects(List<TransformableProject> projects)
        {
            projects.First(p => p.Name == "Core").Name = "AppleCore";
            projects.First(p => p.Name == "Test.Unit").Name = "Testing.Of.Units";
            projects.First(p => p.Name == "Presentation.Web").Name = "Presentation.Internet";
            projects.First(p => p.Name == "Infrastructure.NHibernate").Name = "Client.Infrastructure.ORM";
            projects.First(p => p.Name == "Infrastructure.Migrations").Name = "DB.Migrations";
        }

        [Test]
        public void GetAssembledSolution_should_return_solution_file_as_string()
        {
            var sln = _assembler.GetAssembledSolution(FormatVersion, VisualStudioVersion);
            var fixture = TestUtility.GetFileContents("ExpectedAssembly.sln");
            Assert.AreEqual(fixture, sln);
        }

        [Test]
        public void Assemble_should_overwrite_original_solution_file()
        {
            //arrange
            var info = new ParserInfo(TestUtility.GetFixturePath("NDrivenCopy.sln"));
            var assembler = new SolutionAssembler(info);
            SetUpProjects(info.GetProjects());

            //act
            assembler.Assemble("NewSolution", FormatVersion, VisualStudioVersion);

            //assert
            var fixture = TestUtility.GetFileContents("ExpectedAssembly.sln");
            Assert.True(File.Exists(TestUtility.GetFixturePath("NewSolution.sln")));
            Assert.AreEqual(fixture, TestUtility.GetFileContents("NewSolution.sln"));
        }

        [Test]
        public void Assemble_should_overrwrite_contents_if_name_not_given()
        {
            //arrange
            var info = new ParserInfo(TestUtility.GetFixturePath("NDrivenCopy.sln"));
            var assembler = new SolutionAssembler(info);
            SetUpProjects(info.GetProjects());

            //act
            assembler.Assemble(FormatVersion, VisualStudioVersion);

            //assert
            var fixture = TestUtility.GetFileContents("ExpectedAssembly.sln");
            Assert.AreEqual(fixture, TestUtility.GetFileContents("NDrivenCopy.sln"));
        }

        private static void SetUpFiles()
        {
            var original = new FileInfo(TestUtility.GetFixturePath("NDriven.sln"));
            var copy = original.DirectoryName + Path.DirectorySeparatorChar + "NDrivenCopy.sln";
            var newSln = original.DirectoryName + Path.DirectorySeparatorChar + "NewSolution.sln";
            if (File.Exists(copy))
                File.Delete(copy);
            if (File.Exists(newSln))
                File.Delete(newSln);
            File.Copy(original.FullName, copy);
        }
    }
}
