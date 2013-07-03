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
        }

        public string Name
        {
            get { return _solutionProject.Name; }
            set
            {
                _solutionProject.Name = value;
                NameIsModified = true;
            }
        }

        public string Path
        {
            get { return _solutionProject.Path; }
            set
            {
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

        public bool NameIsModified { get; private set; }
        public bool PathIsModified { get; private set; }
    }
}
