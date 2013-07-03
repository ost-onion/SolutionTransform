using System.IO;

namespace Onion.SolutionTransform
{
    public class Transformer
    {
        public Transformer(string solutionPath)
        {
            if (!File.Exists(solutionPath))
                throw new FileNotFoundException(string.Format("Solution file {0} does not exist", solutionPath));
            SolutionPath = solutionPath;
            BasePath = new FileInfo(SolutionPath).DirectoryName;
        }

        public string SolutionPath { get; private set; }
        public string BasePath { get; private set; }
    }
}
