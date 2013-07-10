using System.Collections.Generic;
using Onion.SolutionTransform.Parser;
using Onion.SolutionTransform.Solution;
using Onion.SolutionTransform.Strategy;
using Onion.SolutionTransform.Template;

namespace Onion.SolutionTransform
{
    public class Transformer : ITransformer
    {
        private ISolutionAssembler _assembler;

        public Transformer(string slnPath)
        {
            ParserInfo = new ParserInfo(slnPath);
            _assembler = new SolutionAssembler(ParserInfo);
            Strategies = new Queue<ISolutionTransformStrategy>();
        }

        public void Transform(string formatVersion = "12.00", string visualStudioVersion = "2012")
        {
            foreach(var strat in Strategies)
                strat.Transform();
            _assembler.Assemble(formatVersion, visualStudioVersion);
        }

        public ITransformer AddStrategy(ISolutionTransformStrategy strat)
        {
            if (strat.ParserInfo == null)
                strat.ParserInfo = ParserInfo;
            Strategies.Enqueue(strat);
            return this;
        }

        public ITransformer AddTemplate(ISolutionTransformTemplate template)
        {
            foreach (var strat in template.GetStrategies())
                AddStrategy(strat);
            return this;
        }

        public IParserInfo ParserInfo { get; private set; }
        public Queue<ISolutionTransformStrategy> Strategies { get; private set; }
    }
}
