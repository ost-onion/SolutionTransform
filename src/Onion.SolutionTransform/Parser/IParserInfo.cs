using System.Collections.Generic;
using Onion.SolutionParser.Parser.Model;

namespace Onion.SolutionTransform.Parser
{
    public interface IParserInfo
    {
        ISolution GetSolution();
        List<TransformableProject> GetProjects();
        string SolutionPath { get; }
        string BasePath { get; }
    }
}
