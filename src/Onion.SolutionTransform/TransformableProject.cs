using System;
using Onion.SolutionParser.Parser.Model;

namespace Onion.SolutionTransform
{
    public class TransformableProject
    {
        private readonly Project _solutionProject;

        public TransformableProject(Project proj)
        {
            _solutionProject = proj;
            IsModified = false;
        }

        public string Name
        {
            get { return _solutionProject.Name; }
            set
            {
                _solutionProject.Name = value;
                IsModified = true;
            }
        }

        public string Path
        {
            get { return _solutionProject.Path; }
            set
            {
                _solutionProject.Path = value;
                IsModified = true;
            }
        }

        public Guid TypeGuid
        {
            get { return _solutionProject.TypeGuid; }
        }

        public Guid Guid
        {
            get { return _solutionProject.Guid; }
        }

        public bool IsModified { get; private set; }
    }
}
