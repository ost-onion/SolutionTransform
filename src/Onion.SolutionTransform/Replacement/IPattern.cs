using System.Text.RegularExpressions;

namespace Onion.SolutionTransform.Replacement
{
    public interface IPattern
    {
        Regex Search { get; }
        string Replace { get; }
    }
}
