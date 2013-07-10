using System;
using System.Text;
using Onion.SolutionTransform.Parser;

namespace Onion.SolutionTransform.Solution
{
    public class SolutionAssembler
    {
        private readonly IParserInfo _info;

        public SolutionAssembler(IParserInfo info)
        {
            _info = info;
        }

        public string GetAssembledSolution(string formatVersion, string visualStudioVersion)
        {
            var header = GetSolutionHeader(formatVersion, visualStudioVersion);
            var builder = new StringBuilder(header);
            ProjectAssembler.Assemble(builder, _info);
            var solution = _info.GetSolution();
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
