using System.Collections.Generic;
using Onion.SolutionParser.Parser.Model;

namespace Onion.SolutionTransform
{
    public interface ITransformer
    {
        IEnumerable<Project> GetProjects();
        string SolutionPath { get; }
        string BasePath { get; }
    }
}
