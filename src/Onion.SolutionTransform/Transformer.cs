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
        private Dictionary<Guid, Project> _projects; 

        public Transformer(string solutionPath)
        {
            if (!File.Exists(solutionPath))
                throw new FileNotFoundException(string.Format("Solution file {0} does not exist", solutionPath));
            SolutionPath = solutionPath;
            BasePath = new FileInfo(SolutionPath).DirectoryName;
            _projects = new Dictionary<Guid, Project>();
        }

        public Dictionary<Guid, Project> GetProjects()
        {
            if (_projects.Count > 0) return _projects;
            var sln = Parser.Parse(SolutionPath);
            var dict = new Dictionary<Guid, Project>();
            sln.Projects.ToList().ForEach(i => dict.Add(i.Guid, i));
            _projects = dict;
            return _projects;
        }

        public string SolutionPath { get; private set; }
        public string BasePath { get; private set; }
    }
}
