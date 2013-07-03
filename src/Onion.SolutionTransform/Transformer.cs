using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Onion.SolutionParser.Parser.Model;
using Parser = Onion.SolutionParser.Parser.SolutionParser;

namespace Onion.SolutionTransform
{
    public class Transformer
    {
        private IEnumerable<Project> _projects; 

        public Transformer(string solutionPath)
        {
            if (!File.Exists(solutionPath))
                throw new FileNotFoundException(string.Format("Solution file {0} does not exist", solutionPath));
            SolutionPath = solutionPath;
            BasePath = new FileInfo(SolutionPath).DirectoryName;
        }

        public IEnumerable<Project> GetProjects()
        {
            if (_projects != null) return _projects;
            var sln = Parser.Parse(SolutionPath);
            _projects = sln.Projects;
            return _projects;
        }

        public string SolutionPath { get; private set; }
        public string BasePath { get; private set; }
    }
}
