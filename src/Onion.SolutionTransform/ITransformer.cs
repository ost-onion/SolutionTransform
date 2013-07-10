using System.Collections.Generic;
using Onion.SolutionTransform.Parser;
using Onion.SolutionTransform.Strategy;

namespace Onion.SolutionTransform
{
    public interface ITransformer
    {
        ITransformer AddStrategy(ISolutionTransformStrategy strat);
        IParserInfo ParserInfo { get; }
        Queue<ISolutionTransformStrategy> Strategies { get; }  
    }
}
