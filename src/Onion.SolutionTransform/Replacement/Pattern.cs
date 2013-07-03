using System.Text.RegularExpressions;

namespace Onion.SolutionTransform.Replacement
{
    public class Pattern : IPattern
    {
        public Pattern(string pattern, string replace)
        {
            Search = new Regex(pattern, RegexOptions.Compiled);
            Replace = replace;
        }

        public Regex Search { get; private set; }
        public string Replace { get; set; }
    }
}
