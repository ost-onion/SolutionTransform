using System;
using NUnit.Framework;
using Onion.SolutionParser.Parser.Model;

namespace Onion.SolutionTransform.Tests
{
    [TestFixture]
    public class TransformableProjectTest
    {
        private Project _solutionProject;
        private TransformableProject _project;

        [SetUp]
        public void SetUp()
        {
            _solutionProject = new Project(Guid.NewGuid(), "TestProject", "src/TestProject/TestProject.csproj",
                                       Guid.NewGuid());
            _project = new TransformableProject(_solutionProject);
        }

        [Test]
        public void IsModified_should_default_to_false()
        {
            Assert.False(_project.IsModified);
        }

        [Test]
        public void Name_getter_should_delegate_to_underlying_Project()
        {
            Assert.AreEqual(_solutionProject.Name, _project.Name);
        }

        [Test]
        public void Name_setter_should_set_underlying_and_set_IsModified_property()
        {
            _project.Name = "NewProject";
            Assert.AreEqual("NewProject", _project.Name);
            Assert.True(_project.IsModified);
        }

        [Test]
        public void Path_getter_should_delegate_to_underlying_Project()
        {
            Assert.AreEqual(_solutionProject.Path, _project.Path);
        }

        [Test]
        public void Path_setter_should_set_underlying_and_set_IsModified_property()
        {
            _project.Path = "src/different/path";
            Assert.AreEqual("src/different/path", _project.Path);
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
