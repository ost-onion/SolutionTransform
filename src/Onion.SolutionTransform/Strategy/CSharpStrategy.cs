using System.Collections.Generic;
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
            var usng = string.Format("using[\\s]+{0}", p.PreviousName);
            var nspace = string.Format("namespace[\\s]+{0}", p.PreviousName);
            var qualified = string.Format("\\b{0}\\.", p.PreviousName);
            patterns.Add(new Pattern(usng, string.Format("using {0}", p.Name)));
            patterns.Add(new Pattern(nspace, string.Format("namespace {0}", p.Name)));
            patterns.Add(new Pattern(qualified, string.Format("{0}.", p.Name)));
        }
    }
}
