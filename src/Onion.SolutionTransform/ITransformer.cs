using System.Collections.Generic;
using Onion.SolutionTransform.Parser;
using Onion.SolutionTransform.Strategy;
using Onion.SolutionTransform.Template;

namespace Onion.SolutionTransform
{
    public interface ITransformer
    {
        void Transform(string formatVersion, string visualStudioVersion);
        ITransformer AddStrategy(ISolutionTransformStrategy strat);
        ITransformer AddTemplate(ISolutionTransformTemplate template);
        IParserInfo ParserInfo { get; }
        Queue<ISolutionTransformStrategy> Strategies { get; }  
    }
}
