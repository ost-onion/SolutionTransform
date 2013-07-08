using Onion.SolutionTransform.Parser;

namespace Onion.SolutionTransform
{
    public interface ITransformer
    {
        IParserInfo ParserInfo { get; }
    }
}
