using System.Threading.Tasks;
using Onion.SolutionTransform.Parser;

namespace Onion.SolutionTransform.Strategy
{
    public interface ISolutionTransformStrategy
    {
        Task TransformAsync();
        IParserInfo ParserInfo { get; set; }
    }
}
