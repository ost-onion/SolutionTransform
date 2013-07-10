using System.Collections.Generic;
using Onion.SolutionTransform.Strategy;

namespace Onion.SolutionTransform.Template
{
    public interface ITemplate
    {
        IEnumerable<ISolutionTransformStrategy> GetStrategies();
    }
}
