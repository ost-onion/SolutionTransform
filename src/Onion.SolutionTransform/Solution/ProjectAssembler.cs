using System;
using System.Text;
using Onion.SolutionTransform.Parser;
using Onion.SolutionTransform.Project;

namespace Onion.SolutionTransform.Solution
{
    internal static class ProjectAssembler
    {
        public static void Assemble(StringBuilder builder, IParserInfo info)
        {
            var projects = info.GetProjects();
            foreach (var project in projects)
            {
                AssembleHeader(builder, project);
                AssembleProjectSection(builder, project);
                builder.Append("EndProject" + Environment.NewLine);
            }
        }

        private static void AssembleProjectSection(StringBuilder builder, TransformableProject project)
        {
            if (project.ProjectSection == null) return;
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

        private static void AssembleHeader(StringBuilder builder, TransformableProject project)
        {
            builder.AppendFormat("Project(\"{0}\") = \"{1}\", \"{2}\", \"{3}\"" + Environment.NewLine,
                                 '{' + project.TypeGuid.ToString().ToUpperInvariant() + '}',
                                 project.Name,
                                 !string.IsNullOrEmpty(project.PreviousName)
                                     ? project.Path.Replace(project.PreviousName, project.Name)
                                     : project.Path,
                                 '{' + project.Guid.ToString().ToUpperInvariant() + '}');
        }
    }
}
