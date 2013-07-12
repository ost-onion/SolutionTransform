using System.Collections.Generic;
using System.IO;
using System.Linq;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;

namespace Onion.SolutionTransform.Replacement
{
    public class CSharpReplacement
    {
        private readonly string _path;
        private readonly string _search;
        private readonly string _replace;

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
            root = ReplaceUsingDirectives(root);
            root = ReplaceNamespaceDeclarations(root);
            root = ReplaceBaseLists(root);
            File.WriteAllText(_path, root.GetText().ToString());
        }

        private CompilationUnitSyntax ReplaceBaseLists(CompilationUnitSyntax root)
        {
            var bases = root.DescendantNodes().OfType<BaseListSyntax>();
            var nodes = new List<QualifiedNameSyntax>();
            foreach (var t in bases.Select(b => b.Types).SelectMany(types => types))
                nodes.AddRange(t.DescendantNodes().OfType<QualifiedNameSyntax>().Where(q => q.Left.ToFullString().StartsWith(_search)));
            return root.ReplaceNodes(nodes,
                                     (n1, n2) =>
                                     n1.WithLeft(Syntax.ParseName(n1.Left.ToFullString().Replace(_search, _replace))));
        }

        private CompilationUnitSyntax ReplaceNamespaceDeclarations(CompilationUnitSyntax root)
        {
            var oldNodes = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().Where(n => n.Name.ToFullString().StartsWith(_search));
            return root.ReplaceNodes(oldNodes,
                                     (n1, n2) =>
                                     n1.WithName(Syntax.ParseName(n1.Name.ToFullString().Replace(_search, _replace))));
        }

        private CompilationUnitSyntax ReplaceUsingDirectives(CompilationUnitSyntax root)
        {
            var oldNodes = root.Usings.Where(u => u.Name.ToFullString().StartsWith(_search));
            return root.ReplaceNodes(oldNodes,
                                     (n1, n2) =>
                                     n1.WithName(Syntax.ParseName(n1.Name.ToFullString().Replace(_search, _replace))));
        }
    }
}
