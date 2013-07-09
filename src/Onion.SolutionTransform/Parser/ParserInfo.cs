using System.Collections.Generic;
using System.IO;
using System.Linq;
using Onion.SolutionParser.Parser.Model;
using Onion.SolutionTransform.Project;
using OnionParser = Onion.SolutionParser.Parser.SolutionParser;

namespace Onion.SolutionTransform.Parser
{
    public class ParserInfo : IParserInfo
    {
        private ISolution _sln;
        private List<TransformableProject> _projects; 

        public ParserInfo(string solutionPath)
        {
            if (!File.Exists(solutionPath))
                throw new FileNotFoundException(string.Format("Solution file {0} does not exist", solutionPath));
            SolutionPath = solutionPath;
            BasePath = new FileInfo(SolutionPath).DirectoryName;
        }

        public ISolution GetSolution()
        {
            if (_sln != null) return _sln;
            _sln = OnionParser.Parse(SolutionPath);
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
