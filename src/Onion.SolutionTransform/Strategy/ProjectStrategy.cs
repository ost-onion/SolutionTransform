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
                    doc.Write();
                });

        }
    }
}
