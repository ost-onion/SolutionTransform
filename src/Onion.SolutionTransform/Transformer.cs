using System.Collections.Generic;
using Onion.SolutionTransform.Parser;
using Onion.SolutionTransform.Strategy;
using Onion.SolutionTransform.Template;

namespace Onion.SolutionTransform
{
    public class Transformer : ITransformer
    {
        public Transformer(string slnPath)
        {
            ParserInfo = new ParserInfo(slnPath);
            Strategies = new Queue<ISolutionTransformStrategy>();
        }

        public ITransformer AddStrategy(ISolutionTransformStrategy strat)
        {
            Strategies.Enqueue(strat);
            return this;
        }

        public ITransformer AddTemplate(ITemplate template)
        {
            foreach (var strat in template.GetStrategies())
            {
                Strategies.Enqueue(strat);
            }
            return this;
        }

        public IParserInfo ParserInfo { get; private set; }
        public Queue<ISolutionTransformStrategy> Strategies { get; private set; }
    }
}
