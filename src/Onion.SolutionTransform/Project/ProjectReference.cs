using System;
using System.Xml.Linq;

namespace Onion.SolutionTransform.Project
{
    public class ProjectReference
    {
        private readonly XElement _ref;

        public ProjectReference(XElement elem)
        {
            _ref = elem;
        }

        public string Include
        {
            get { return _ref.Attribute("Include").Value; }
            set { _ref.Attribute("Include").SetValue(value); }
        }

        public Guid Project
        {
            get { return new Guid(_ref.Element(ProjectDocument.XmlNs + "Project").Value); }
        }

        public string Name
        {
            get { return _ref.Element(ProjectDocument.XmlNs + "Name").Value; }
            set { _ref.Element(ProjectDocument.XmlNs + "Name").SetValue(value); }
        }
    }
}
