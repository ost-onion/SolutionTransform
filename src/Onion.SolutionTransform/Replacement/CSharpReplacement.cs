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
            root = ReplaceUsingDirectives(root);
            root = ReplaceNamespaceDeclarations(root);
            File.WriteAllText(_path, root.GetText().ToString());
        }

        private CompilationUnitSyntax ReplaceNamespaceDeclarations(CompilationUnitSyntax root)
        {
            var ns = root.DescendantNodes().Where(n => n.Kind == SyntaxKind.NamespaceDeclaration).Cast<NamespaceDeclarationSyntax>();
            return root.ReplaceNodes(ns.Where(n => n.Name.ToFullString().StartsWith(_search)),
                                     (n1, n2) =>
                                     n1.WithName(Syntax.ParseName(n1.Name.ToFullString().Replace(_search, _replace))));
        }

        private CompilationUnitSyntax ReplaceUsingDirectives(CompilationUnitSyntax root)
        {
            var oldNodes = new List<UsingDirectiveSyntax>();
            oldNodes.AddRange(root.Usings.Where(u => u.Name.ToFullString().StartsWith(_search)));
            return root.ReplaceNodes(oldNodes,
                                     (n1, n2) =>
                                     n1.WithName(Syntax.ParseName(n1.Name.ToFullString().Replace(_search, _replace))));
        }
    }
}
