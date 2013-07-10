namespace Onion.SolutionTransform.Solution
{
    public interface ISolutionAssembler
    {
        string GetAssembledSolution(string formatVersion, string visualStudioVersion);
        void Assemble(string slnName, string formatVersion, string visualStudioVersion);
        void Assemble(string formatVersion, string visualStudioVersion);
    }
}
