using System.IO;
using NUnit.Framework;

namespace Onion.SolutionTransform.Tests
{
    [TestFixture]
    public class TransformerTest
    {
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
            var sln = TestUtility.GetFixturePath(@"ndriven\NDriven.sln");
            var transform = new Transformer(sln);
            Assert.AreEqual(sln, transform.SolutionPath);
        }
    }
}
