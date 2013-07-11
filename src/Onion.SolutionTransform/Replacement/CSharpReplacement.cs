using System.Collections.Generic;
using System.IO;
using System.Linq;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;

namespace Onion.SolutionTransform.Replacement
{
    public class CSharpReplacement
    {
        private string _path;
        private string _search;
        private string _replace;

        public CSharpReplacement(string path, string search, string replace)
        {
            _path = path;
            _search = search;
            _replace = replace;
        }

        public void Replace()
        {
            SyntaxTree tree = SyntaxTree.ParseFile(_path);
            var root = tree.GetRoot();
            var oldNodes = new List<UsingDirectiveSyntax>();
            oldNodes.AddRange(root.Usings.Where(u => u.Name.GetText().ToString().StartsWith(_search)));
            root = root.ReplaceNodes(oldNodes, (n1, n2) => n1.WithName(Syntax.ParseName(n1.Name.GetText().ToString().Replace(_search, _replace))));
            File.WriteAllText(_path, root.GetText().ToString());
        }
    }
}
