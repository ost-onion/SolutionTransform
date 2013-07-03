using System.Text.RegularExpressions;

namespace Onion.SolutionTransform.Replacement
{
    public interface IPattern
    {
        string ReplaceInString(string str);
        Regex Search { get; }
        string Replace { get; }
    }
}
