using System.Collections.Generic;
using Onion.SolutionTransform.Strategy;

namespace Onion.SolutionTransform.Template
{
    public interface ISolutionTransformTemplate
    {
        IEnumerable<ISolutionTransformStrategy> GetStrategies();
    }
}
