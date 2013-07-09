using System.IO;
using System.Xml.Linq;
using Onion.SolutionTransform.Parser;

namespace Onion.SolutionTransform.Project
{
    public class ProjectDocument
    {
        private TransformableProject _project;
        private string _path;
        private readonly XDocument _doc;
        private static readonly XNamespace XmlNs = "http://schemas.microsoft.com/developer/msbuild/2003";

        public ProjectDocument(TransformableProject proj, IParserInfo info)
        {
            _project = proj;
            _path = info.BasePath + Path.DirectorySeparatorChar + proj.Path;
            _doc = XDocument.Load(_path);
        }

        public void Write()
        {
            File.WriteAllText(_path, ToString());
        }

        public override string ToString()
        {
            return _doc.ToString();
        }

        public XElement Project
        {
            get { return _doc.Element(XmlNs + "Project"); }
        }

        public string AssemblyName
        {
            get { return Project.Element(XmlNs + "PropertyGroup").Element(XmlNs + "AssemblyName").Value; }
            set { Project.Element(XmlNs + "PropertyGroup").Element(XmlNs + "AssemblyName").SetValue(value); }
        }

        public string RootNamespace
        {
            get { return Project.Element(XmlNs + "PropertyGroup").Element(XmlNs + "RootNamespace").Value; }
            set { Project.Element(XmlNs + "PropertyGroup").Element(XmlNs + "RootNamespace").SetValue(value); }
        }
    }
}
