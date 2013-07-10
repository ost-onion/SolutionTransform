using System;
using Onion.SolutionParser.Parser.Model;
using ParserProject = Onion.SolutionParser.Parser.Model.Project;

namespace Onion.SolutionTransform.Project
{
    public class TransformableProject
    {
        private readonly ParserProject _solutionProject;
        private static readonly Guid SolutionFolderType = new Guid("2150E333-8FDC-42A3-9474-1A3956D46DE8");

        public TransformableProject(ParserProject proj)
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

        public bool IsSolutionFolder
        {
            get { return TypeGuid == SolutionFolderType; }
        }

        public ProjectSection ProjectSection
        {
            get { return _solutionProject.ProjectSection; }
        }

        public string PreviousName { get; private set; }
        public string PreviousPath { get; private set; }
        public bool NameIsModified { get; private set; }
        public bool PathIsModified { get; private set; }
    }
}
