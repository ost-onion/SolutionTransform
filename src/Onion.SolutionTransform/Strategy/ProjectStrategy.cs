using System.Collections.Generic;
using System.IO;
using System.Linq;
using Onion.SolutionTransform.Project;

namespace Onion.SolutionTransform.Strategy
{
    public class ProjectStrategy : SolutionTransformStrategyBase
    {
        private List<TransformableProject> _documentedProjects;

        public ProjectStrategy()
        {
            _documentedProjects = new List<TransformableProject>();
        }

        public override void Transform()
        {
            var projects = TransformableProjects(p => p.NameIsModified && !p.IsSolutionFolder);
            TransformProjectFileContents(projects);
            TransformProjectPaths(projects);
        }

        private void TransformProjectPaths(List<TransformableProject> projects)
        {
            projects.ForEach(p =>
                {
                    var info = new FileInfo(ParserInfo.BasePath + Path.DirectorySeparatorChar + p.Path);
                    var directory = info.Directory;
                    File.Move(info.FullName, directory.FullName + Path.DirectorySeparatorChar + p.Name + info.Extension);
                    Directory.Move(directory.FullName, directory.FullName.Replace(p.PreviousName, p.Name));
                });
        }

        private void TransformProjectFileContents(List<TransformableProject> projects)
        {
            projects.ForEach(p =>
                {
                    var doc = new ProjectDocument(p, ParserInfo) {RootNamespace = p.Name, AssemblyName = p.Name};
                    WriteProjectReferences(p);
                    doc.Write();
                });
        }

        private void WriteProjectReferences(TransformableProject p)
        {
            DocumentedProjects.ForEach(t =>
                {
                    var doc = new ProjectDocument(t, ParserInfo);
                    var reference = doc.ProjectReferences.FirstOrDefault(pr => pr.Project.Equals(p.Guid));
                    if (reference == null) return;
                    reference.Include = reference.Include.Replace(p.PreviousName, p.Name);
                    reference.Name = p.Name;
                    doc.Write();
                });
        }

        private List<TransformableProject> DocumentedProjects
        {
            get
            {
                if (_documentedProjects.Any()) return _documentedProjects;
                _documentedProjects = TransformableProjects(tp => !tp.IsSolutionFolder);
                return _documentedProjects;
            }
        }
    }
}
