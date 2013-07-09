using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Onion.SolutionTransform.Strategy
{
    public class ProjectStrategy : SolutionTransformStrategyBase
    {
        private static readonly XNamespace XmlNs = "http://schemas.microsoft.com/developer/msbuild/2003";

        public override void Transform()
        {
            var projects = TransformableProjects(p => p.NameIsModified);
            projects.ForEach(p =>
                {
                    var path = ParserInfo.BasePath + Path.DirectorySeparatorChar + p.Path;
                    var doc = XDocument.Load(path);
                    var project = doc.Element(XmlNs + "Project");

                    //update root namespace and assembly name
                    var initialPropertyGroup = project.Elements(XmlNs + "PropertyGroup").First();
                    initialPropertyGroup.Element(XmlNs + "RootNamespace").SetValue(p.Name);
                    initialPropertyGroup.Element(XmlNs + "AssemblyName").SetValue(p.Name);

                    //update project references
                    //var all = ParserInfo.GetProjects();


                    File.WriteAllText(path, doc.ToString());
                });

        }
    }
}
