using System.Text.RegularExpressions;

namespace Onion.SolutionTransform.Replacement
{
    public class NamePattern : IPattern
    {
        public NamePattern(string pattern, string replace)
        {
            Search = new Regex(pattern, RegexOptions.Compiled);
            Replace = replace;
        }

        public Regex Search { get; private set; }
        public string Replace { get; set; }
    }
}
