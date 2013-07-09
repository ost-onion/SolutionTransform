using System.IO;
using System.Linq;
using System.Xml.Linq;
using Onion.SolutionTransform.Project;

namespace Onion.SolutionTransform.Strategy
{
    public class ProjectStrategy : SolutionTransformStrategyBase
    {
        public override void Transform()
        {
            var projects = TransformableProjects(p => p.NameIsModified);
            projects.ForEach(p =>
                {
                    var doc = new ProjectDocument(p, ParserInfo) {RootNamespace = p.Name, AssemblyName = p.Name};
                    var all = TransformableProjects(tp => !tp.IsSolutionFolder);
                    all.ForEach(t =>
                        {
                            var d = new ProjectDocument(t, ParserInfo);
                            var pref = d.ProjectReferences.FirstOrDefault(pr => pr.Project.Equals(p.Guid));
                            if (pref == null) return;
                            pref.Include = pref.Include.Replace(p.PreviousName, p.Name);
                            pref.Name = p.Name;
                            d.Write();
                        });
                    doc.Write();
                });

        }
    }
}
