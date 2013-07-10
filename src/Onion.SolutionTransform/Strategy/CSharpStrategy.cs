using System.Collections.Generic;
using System.Text.RegularExpressions;
using Onion.SolutionTransform.IO;
using Onion.SolutionTransform.Project;
using Onion.SolutionTransform.Replacement;

namespace Onion.SolutionTransform.Strategy
{
    public class CSharpStrategy : PatternBasedStrategy
    {
        protected override List<string> GetFiles()
        {
            var files = DirectoryWalker.GetFiles(ParserInfo.BasePath, "*.cs");
            files.AddRange(DirectoryWalker.GetFiles(ParserInfo.BasePath, "*.cshtml"));
            return files;
        }

        protected override void AddProjectPatterns(TransformableProject p, ISet<IPattern> patterns)
        {
            var qualified = @"(?<!(using|namespace)[\s]+)" + Regex.Escape(p.PreviousName);
            var usng = string.Format("using[\\s]+{0}", Regex.Escape(p.PreviousName));
            var nspace = string.Format("namespace[\\s]+{0}", Regex.Escape(p.PreviousName));
            patterns.Add(new Pattern(qualified, p.Name));
            patterns.Add(new Pattern(usng, string.Format("using {0}", p.Name)));
            patterns.Add(new Pattern(nspace, string.Format("namespace {0}", p.Name)));
        }
    }
}
