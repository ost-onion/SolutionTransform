using NUnit.Framework;
using Onion.SolutionTransform.Replacement;

namespace Onion.SolutionTransform.Tests.Replacement
{
    [TestFixture]
    public class CSharpReplacementTest
    {
        [SetUp]
        public void Setup()
        {
            TestUtility.DeleteFile(@"source\SourceUsingDirectivesCopy.txt");
        }

        [Test]
        public void Replace_should_replace_text_in_using_directives()
        {
            var copy = TestUtility.CopyFile(@"source\SourceUsingDirectives.txt", "SourceUsingDirectivesCopy.txt");
            var expected = TestUtility.GetFileContents(@"output\OutputUsingDirectives.txt");
            var replacement = new CSharpReplacement(copy, "Core", "AppleCore");

            replacement.Replace();

            var output = TestUtility.GetFileContents(@"source\SourceUsingDirectivesCopy.txt");
            Assert.AreEqual(expected, output);
        }
    }
}
