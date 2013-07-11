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

        public static string CopyFile(string fileName, string newName)
        {
            var original = new FileInfo(GetFixturePath(fileName));
            var directory = original.DirectoryName;
            var newPath = directory + Path.DirectorySeparatorChar + newName;
            original.CopyTo(newPath);
            return newPath;
        }

        public static void DeleteFile(string fileName)
        {
            var path = GetFixturePath(fileName);
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}