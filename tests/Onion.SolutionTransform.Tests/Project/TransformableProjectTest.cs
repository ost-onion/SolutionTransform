using System;
using NUnit.Framework;
using Onion.SolutionTransform.Project;
using ParserProject = Onion.SolutionParser.Parser.Model.Project;

namespace Onion.SolutionTransform.Tests.Project
{
    [TestFixture]
    public class TransformableProjectTest
    {
        private ParserProject _solutionProject;
        private TransformableProject _project;

        [SetUp]
        public void SetUp()
        {
            _solutionProject = new ParserProject(Guid.NewGuid(), "TestProject", "src/TestProject/TestProject.csproj",
                                       Guid.NewGuid());
            _project = new TransformableProject(_solutionProject);
        }

        [Test]
        public void NameIsModified_and_PathIsModified_should_default_to_false()
        {
            Assert.False(_project.NameIsModified);
            Assert.False(_project.PathIsModified);
        }

        [Test]
        public void PreviousName_and_PreviousPath_should_default_to_null()
        {
            Assert.Null(_project.PreviousName);
            Assert.Null(_project.PreviousPath);
        }

        [Test]
        public void Name_getter_should_delegate_to_underlying_Project()
        {
            Assert.AreEqual(_solutionProject.Name, _project.Name);
        }

        [Test]
        public void Name_setter_should_set_underlying_and_set_NameIsModified_property_and_set_PreviousName()
        {
            _project.Name = "NewProject";
            Assert.AreEqual("NewProject", _project.Name);
            Assert.AreEqual("TestProject", _project.PreviousName);
            Assert.True(_project.NameIsModified);
        }

        [Test]
        public void Path_getter_should_delegate_to_underlying_Project()
        {
            Assert.AreEqual(_solutionProject.Path, _project.Path);
        }

        [Test]
        public void Path_setter_should_set_underlying_and_set_PathIsModified_property()
        {
            _project.Path = "src/different/path";
            Assert.AreEqual("src/different/path", _project.Path);
            Assert.AreEqual("src/TestProject/TestProject.csproj", _project.PreviousPath);
            Assert.True(_project.PathIsModified);
        }

        [Test]
        public void TypeGuid_getter_should_delegate_to_underlying_Project()
        {
            Assert.AreEqual(_solutionProject.TypeGuid, _project.TypeGuid);
        }

        [Test]
        public void Guid_getter_should_delegate_to_underlying_Project()
        {
            Assert.AreEqual(_solutionProject.Guid, _project.Guid);
        }
    }
}
