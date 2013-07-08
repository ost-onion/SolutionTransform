using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Onion.SolutionTransform.IO;
using Onion.SolutionTransform.Parser;
using Onion.SolutionTransform.Replacement;

namespace Onion.SolutionTransform.Strategy
{
    public class CSharpStrategy : ISolutionTransformStrategy
    {
        public async Task TransformAsync()
        {
            var transformableProjects = TransformableProjects(p => p.NameIsModified);
            if (!transformableProjects.Any()) new CancellationTokenSource().Cancel();
            var patterns = GetPatternSet(transformableProjects);
            var files = DirectoryWalker.GetFiles(ParserInfo.BasePath, "*.cs");
            files.AddRange(DirectoryWalker.GetFiles(ParserInfo.BasePath, "*.cshtml"));
            await DoTransformAsync(files, patterns);
        }

        private static async Task DoTransformAsync(List<string> files, IEnumerable<IPattern> patterns)
        {
            await Task.Run(() => files.ForEach(f =>
                {
                    string contents;
                    using (var reader = new StreamReader(f))
                    {
                        contents = reader.ReadToEnd();
                    }
                    contents = patterns.Aggregate(contents, (current, pattern) => pattern.ReplaceInString(current));
                    File.WriteAllText(f, contents);
                }));
        }

        private static IEnumerable<IPattern> GetPatternSet(List<TransformableProject> transformableProjects)
        {
            var patterns = new HashSet<IPattern>();
            transformableProjects.ForEach(p =>
                {
                    var usng = string.Format("using[\\s]+{0}", p.PreviousName);
                    var nspace = string.Format("namespace[\\s]+{0}", p.PreviousName);
                    patterns.Add(new Pattern(usng, string.Format("using {0}", p.Name)));
                    patterns.Add(new Pattern(nspace, string.Format("namespace {0}", p.Name)));
                });
            return patterns;
        }

        protected List<TransformableProject> TransformableProjects(Func<TransformableProject, bool> predicate)
        {
            var modified = ParserInfo.GetProjects().Where(predicate);
            var transformableProjects = modified as List<TransformableProject> ?? modified.ToList();
            return transformableProjects;
        }

        public IParserInfo ParserInfo { get; set; }
    }
}
