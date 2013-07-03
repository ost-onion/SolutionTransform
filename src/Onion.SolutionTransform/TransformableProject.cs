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
            NameIsModified = false;
            PathIsModified = false;
            PreviousName = null;
            PreviousPath = null;
        }

        public string Name
        {
            get { return _solutionProject.Name; }
            set
            {
                PreviousName = _solutionProject.Name;
                _solutionProject.Name = value;
                NameIsModified = true;
            }
        }

        public string Path
        {
            get { return _solutionProject.Path; }
            set
            {
                PreviousPath = _solutionProject.Path;
                _solutionProject.Path = value;
                PathIsModified = true;
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

        public string PreviousName { get; private set; }
        public string PreviousPath { get; private set; }
        public bool NameIsModified { get; private set; }
        public bool PathIsModified { get; private set; }
    }
}
