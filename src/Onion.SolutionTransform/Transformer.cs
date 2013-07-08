using Onion.SolutionTransform.Parser;

namespace Onion.SolutionTransform
{
    public class Transformer : ITransformer
    {
        public Transformer(string slnPath)
        {
            ParserInfo = new ParserInfo(slnPath);
        }

        public IParserInfo ParserInfo { get; private set; }
    }
}
