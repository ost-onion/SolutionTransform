using NUnit.Framework;
using Onion.SolutionTransform.IO;

namespace Onion.SolutionTransform.Tests.IO
{
    [TestFixture]
    public class DirectoryWalkerTest
    {
        [Test]
        public void GetFiles_should_return_all_files_in_a_directory()
        {
            var files = DirectoryWalker.GetFiles(TestUtility.GetFixturePath("ndriven"));
            Assert.AreEqual(128, files.Count);
        }

        [Test]
        public void GetFiles_should_return_all_files_given_a_wildcard()
        {
            var files = DirectoryWalker.GetFiles(TestUtility.GetFixturePath("ndriven"), "*.cs");
            Assert.AreEqual(74, files.Count);
        }
    }
}
