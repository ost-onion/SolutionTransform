using System;
using System.IO;
using System.Text;
using Onion.SolutionTransform.Parser;

namespace Onion.SolutionTransform.Solution
{
    public class SolutionAssembler : ISolutionAssembler
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
            GlobalAssembler.Assemble(builder, _info);
            return builder.ToString();
        }

        public void Assemble(string slnName, string formatVersion, string visualStudioVersion)
        {
            Assemble(formatVersion, visualStudioVersion);
            File.Move(_info.SolutionPath, _info.BasePath + Path.DirectorySeparatorChar + slnName + ".sln");
        }

        public void Assemble(string formatVersion, string visualStudioVersion)
        {
            var assembled = GetAssembledSolution(formatVersion, visualStudioVersion);
            File.WriteAllText(_info.SolutionPath, assembled);
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
