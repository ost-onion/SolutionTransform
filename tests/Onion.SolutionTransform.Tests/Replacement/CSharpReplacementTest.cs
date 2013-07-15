using System.IO;
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
            TestUtility.DeleteFile(@"source\SourceTypeConstraintCopy.txt");
            TestUtility.DeleteFile(@"source\SourceAllCopy.txt");
            TestUtility.DeleteFile(@"source\SourceTypeArgumentsCopy.txt");
            TestUtility.DeleteFile(@"source\SourceVariableDeclarationCopy.txt");
            TestUtility.DeleteFile(@"source\SourceObjectCreationExpressionCopy.txt");
        }

        [Test]
        public void Replace_should_replace_text_in_using_directives()
        {
            AssertReplacement(@"source\SourceUsingDirectives.txt", "Core", "AppleCore");
        }

        [Test]
        public void Replace_should_replace_text_in_namespace_directive()
        {
            AssertReplacement(@"source\SourceNamespaceDeclaration.txt", "Infrastructure", "Support.Infrastructure");
        }

        [Test]
        public void Replace_should_replace_text_in_a_BaseList()
        {
            AssertReplacement(@"source\SourceBaseList.txt", "Core", "AppleCore");
        }

        [Test]
        public void Replace_should_replace_text_in_TypeConstraints()
        {
            AssertReplacement(@"source\SourceTypeConstraint.txt", "Core", "AppleCore");
        }

        [Test]
        public void Replace_should_replace_text_in_TypeArguments()
        {
            AssertReplacement(@"source\SourceTypeArguments.txt", "Core", "AppleCore");
        }

        [Test]
        public void Replace_should_replace_text_in_VariableDeclarations()
        {
            AssertReplacement(@"source\SourceVariableDeclaration.txt", "Core", "AppleCore");
        }

        [Test]
        public void Replace_should_replace_text_in_ObjectCreationExpression()
        {
            AssertReplacement(@"source\SourceObjectCreationExpression.txt", "Core", "AppleCore");
        }

        [Test]
        public void Replace_should_replace_multiple_constructs()
        {
            AssertReplacement(@"source\SourceAll.txt", "Core", "AppleCore");
        }

        public static void AssertReplacement(string source, string search, string replace)
        {
            var info = new FileInfo(TestUtility.GetFixturePath(source));
            var copyName = info.Name.Replace(info.Extension, "") + "Copy" + info.Extension;
            var outputName = source.Replace("source", "output");
            outputName = outputName.Replace(info.Extension, "");
            outputName = outputName.Replace("Source", "Output") +info.Extension;

            var copy = TestUtility.CopyFile(source, copyName);
            var expected = TestUtility.GetFileContents(outputName);
            var replacement = new CSharpReplacement(copy, search, replace);

            replacement.Replace();

            var output = TestUtility.GetFileContents(@"source\" + copyName);
            Assert.AreEqual(expected, output);
        }
    }
}
