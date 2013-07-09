using Onion.SolutionTransform.Parser;

namespace Onion.SolutionTransform.Strategy
{
    public interface ISolutionTransformStrategy
    {
        void Transform();
        IParserInfo ParserInfo { get; set; }
    }
}
