using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Onion.SolutionTransform.IO;

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
                    var initialPropertyGroup = doc.Element(XmlNs + "Project").Elements(XmlNs + "PropertyGroup").First();
                    initialPropertyGroup.Element(XmlNs + "RootNamespace").SetValue(p.Name);
                    initialPropertyGroup.Element(XmlNs + "AssemblyName").SetValue(p.Name);
                    File.WriteAllText(path, doc.ToString());
                });

        }
    }
}
