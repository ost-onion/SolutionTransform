using System;
using System.Collections.Generic;
using System.Linq;
using Onion.SolutionTransform.Parser;

namespace Onion.SolutionTransform.Strategy
{
    abstract public class SolutionTransformStrategyBase : ISolutionTransformStrategy
    {
        public abstract void Transform();
        public IParserInfo ParserInfo { get; set; }

        protected List<TransformableProject> TransformableProjects(Func<TransformableProject, bool> predicate)
        {
            var modified = ParserInfo.GetProjects().Where(predicate);
            var transformableProjects = modified as List<TransformableProject> ?? modified.ToList();
            return transformableProjects;
        }
    }
}
