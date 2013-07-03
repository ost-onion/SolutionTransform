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
        }

        public string SolutionPath { get; private set; }
    }
}
