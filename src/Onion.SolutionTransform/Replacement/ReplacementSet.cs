using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Onion.SolutionTransform.Replacement
{
    public class ReplacementSet : HashSet<IPattern>
    {
        private readonly Transformer _transformer;

        public ReplacementSet(Transformer t)
        {
            _transformer = t;
            var projects = _transformer.GetProjects().Where(p => p.NameIsModified);
            projects.ToList().ForEach(p =>
                {
                    var pattern = string.Format("\\b{0}\\b", p.PreviousName);
                    Add(new NamePattern(pattern, p.Name));
                });
        }
    }
}
