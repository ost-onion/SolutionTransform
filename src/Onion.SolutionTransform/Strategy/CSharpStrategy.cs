using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Onion.SolutionTransform.IO;
using Onion.SolutionTransform.Replacement;

namespace Onion.SolutionTransform.Strategy
{
    public class CSharpStrategy : SolutionTransformStrategyBase
    {
        public override async Task TransformAsync()
        {
            await DoTransformAsync(GetFiles(), GetPatternSet());
        }

        public override void Transform()
        {
            var patterns = GetPatternSet();
            var enumerable = patterns as IPattern[] ?? patterns.ToArray();
            if (!enumerable.Any()) return;
            Transform(GetFiles(), enumerable);
        }

        protected void Transform(List<string> files, IEnumerable<IPattern> patterns)
        {
            files.ForEach(f =>
            {
                string contents;
                using (var reader = new StreamReader(f))
                {
                    contents = reader.ReadToEnd();
                }
                contents = patterns.Aggregate(contents, (current, pattern) => pattern.ReplaceInString(current));
                File.WriteAllText(f, contents);
            });
        }

        private IEnumerable<IPattern> GetPatternSet()
        {
            var patterns = new HashSet<IPattern>();
            var transformableProjects = TransformableProjects(p => p.NameIsModified);
            if (!transformableProjects.Any()) return patterns;
            transformableProjects.ForEach(p => AddProjectPatterns(p, patterns));
            return patterns;
        }

        private List<string> GetFiles()
        {
            var files = DirectoryWalker.GetFiles(ParserInfo.BasePath, "*.cs");
            files.AddRange(DirectoryWalker.GetFiles(ParserInfo.BasePath, "*.cshtml"));
            return files;
        }

        private async Task DoTransformAsync(List<string> files, IEnumerable<IPattern> patterns)
        {
            var enumerable = patterns as IPattern[] ?? patterns.ToArray();
            if (!enumerable.Any()) new CancellationTokenSource().Cancel();
            await Task.Run(() => Transform(files, enumerable));
        }

        private void AddProjectPatterns(TransformableProject p, ISet<IPattern> patterns)
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
