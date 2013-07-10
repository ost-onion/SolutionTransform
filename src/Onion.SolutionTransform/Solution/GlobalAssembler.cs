using System;
using System.Text;
using Onion.SolutionParser.Parser.Model;
using Onion.SolutionTransform.Parser;

namespace Onion.SolutionTransform.Solution
{
    internal static class GlobalAssembler
    {
        public static void Assemble(StringBuilder builder, IParserInfo info)
        {
            var solution = info.GetSolution();
            builder.Append("Global" + Environment.NewLine);
            AssembleSections(builder, solution);
            builder.Append("EndGlobal" + Environment.NewLine);
        }

        private static void AssembleSections(StringBuilder builder, ISolution solution)
        {
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
        }
    }
}
