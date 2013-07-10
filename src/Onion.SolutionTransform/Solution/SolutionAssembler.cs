using System;
using System.Text;
using Onion.SolutionTransform.Parser;

namespace Onion.SolutionTransform.Solution
{
    public class SolutionAssembler
    {
        private IParserInfo _info;

        public SolutionAssembler(IParserInfo info)
        {
            _info = info;
        }

        public string GetAssembledSolution(string formatVersion, string visualStudioVersion)
        {
            var header = GetSolutionHeader(formatVersion, visualStudioVersion);
            var solution = _info.GetSolution();
            var projects = _info.GetProjects();
            var builder = new StringBuilder(header);
            foreach (var project in projects)
            {
                builder.AppendFormat("Project(\"{0}\") = \"{1}\", \"{2}\", \"{3}\"" + Environment.NewLine,
                                     '{' + project.TypeGuid.ToString().ToUpperInvariant() + '}',
                                     project.Name,
                                     !string.IsNullOrEmpty(project.PreviousName) ? project.Path.Replace(project.PreviousName, project.Name) : project.Path,
                                     '{' + project.Guid.ToString().ToUpperInvariant() + '}');
                if (project.ProjectSection != null)
                {
                    builder.AppendFormat("\tProjectSection({0}) = {1}" + Environment.NewLine,
                                         project.ProjectSection.Name,
                                         char.ToLowerInvariant(project.ProjectSection.Type.ToString()[0]) +
                                         project.ProjectSection.Type.ToString().Substring(1));
                    foreach (var entry in project.ProjectSection.Entries)
                    {
                        builder.AppendFormat("\t\t{0} = {1}" + Environment.NewLine, entry.Key, entry.Value);
                    }
                    builder.Append("\tEndProjectSection" + Environment.NewLine);
                }
                builder.Append("EndProject" + Environment.NewLine);
            }
            builder.Append("Global" + Environment.NewLine);
            foreach (var section in solution.Global)
            {
                builder.AppendFormat("\tGlobalSection({0}) = {1}" + Environment.NewLine,
                                     section.Name,
                                     char.ToLowerInvariant(section.Type.ToString()[0]) +
                                     section.Type.ToString().Substring(1));
                foreach (var entry in section.Entries)
                {
                    builder.AppendFormat("\t\t{0} = {1}" + Environment.NewLine, entry.Key, entry.Value);
                }
                builder.Append("\tEndGlobalSection" + Environment.NewLine);
            }
            builder.Append("EndGlobal" + Environment.NewLine);
            return builder.ToString();
        }

        private static string GetSolutionHeader(string formatVersion, string visualStudioVersion)
        {
            var header = Environment.NewLine +
                         string.Format("Microsoft Visual Studio Solution File, Format Version {0}", formatVersion) +
                         Environment.NewLine +
                         string.Format("# Visual Studio {0}", visualStudioVersion) +
                         Environment.NewLine;
            return header;
        }
    }
}
