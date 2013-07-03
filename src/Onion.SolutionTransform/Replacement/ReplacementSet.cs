using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Onion.SolutionTransform.Replacement
{
    public class ReplacementSet : HashSet<Pattern>
    {
        private readonly Transformer _transformer;

        public ReplacementSet(Transformer t)
        {
            _transformer = _transformer;
            var projects = _transformer.GetProjects().Where(p => p.NameIsModified);
            projects.ToList().ForEach(p => Add(new Pattern(string.Format("\b{0}\b", p.PreviousName), p.Name)));
        }
    }
}
