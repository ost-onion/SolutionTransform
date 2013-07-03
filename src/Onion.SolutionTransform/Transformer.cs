using System.Collections.Generic;
using System.IO;
using System.Linq;
using Onion.SolutionParser.Parser.Model;
using Parser = Onion.SolutionParser.Parser.SolutionParser;

namespace Onion.SolutionTransform
{
    public class Transformer : ITransformer
    {
        private ISolution _sln;
        private List<TransformableProject> _projects; 

        public Transformer(string solutionPath)
        {
            if (!File.Exists(solutionPath))
                throw new FileNotFoundException(string.Format("Solution file {0} does not exist", solutionPath));
            SolutionPath = solutionPath;
            BasePath = new FileInfo(SolutionPath).DirectoryName;
        }

        public ISolution GetSolution()
        {
            if (_sln != null) return _sln;
            _sln = Parser.Parse(SolutionPath);
            return _sln;
        }

        public List<TransformableProject> GetProjects()
        {
            if (_projects != null) return _projects;
            var sprojects = GetSolution().Projects.ToList();
            _projects = new List<TransformableProject>();
            sprojects.ForEach(sp => _projects.Add(new TransformableProject(sp)));
            return _projects;
        }

        public string SolutionPath { get; private set; }
        public string BasePath { get; private set; }
    }
}
