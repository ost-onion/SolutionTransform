using System.IO;

namespace Onion.SolutionTransform.Tests
{
    public static class TestUtility
    {
        public static string GetFixturePath(string fileName)
        {
            var fixturesPath = Path.GetFullPath("Fixtures");
            var realName = fileName.Replace('\\', Path.DirectorySeparatorChar);
            return fixturesPath + Path.DirectorySeparatorChar + realName;
        }

        public static string GetFileContents(string fileName)
        {
            var path = GetFixturePath(fileName);
            using (var reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }
    }
}