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
            TestUtility.DeleteFile(@"source\SourceNamespaceDeclarationCopy.txt");
            TestUtility.DeleteFile(@"source\SourceBaseListCopy.txt");
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

        [Test]
        public void Replace_should_replace_text_in_namespace_directive()
        {
            var copy = TestUtility.CopyFile(@"source\SourceNamespaceDeclaration.txt", "SourceNamespaceDeclarationCopy.txt");
            var expected = TestUtility.GetFileContents(@"output\OutputNamespaceDeclaration.txt");
            var replacement = new CSharpReplacement(copy, "Infrastructure", "Support.Infrastructure");

            replacement.Replace();

            var output = TestUtility.GetFileContents(@"source\SourceNamespaceDeclarationCopy.txt");
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void Replace_should_replace_text_in_a_BaseList()
        {
            var copy = TestUtility.CopyFile(@"source\SourceBaseList.txt", "SourceBaseListCopy.txt");
            var expected = TestUtility.GetFileContents(@"output\OutputBaseList.txt");
            var replacement = new CSharpReplacement(copy, "Core", "AppleCore");

            replacement.Replace();

            var output = TestUtility.GetFileContents(@"source\SourceBaseListCopy.txt");
            Assert.AreEqual(expected, output);
        }
    }
}
