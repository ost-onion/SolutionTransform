using System;
using System.Collections.Generic;
using Onion.SolutionParser.Parser.Model;

namespace Onion.SolutionTransform
{
    public interface ITransformer
    {
        ISolution GetSolution();
        List<TransformableProject> GetProjects();
        string SolutionPath { get; }
        string BasePath { get; }
    }
}
