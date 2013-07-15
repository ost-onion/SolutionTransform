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
            root = ReplaceTypeConstraints(root);
            root = ReplaceTypeArguments(root);
            File.WriteAllText(_path, root.GetText().ToString());
        }

        private CompilationUnitSyntax ReplaceTypeArguments(CompilationUnitSyntax root)
        {
            var typeArgs = root.DescendantNodes().OfType<TypeArgumentListSyntax>().Where(t => t.Arguments.Any(a => a.ToFullString().StartsWith("Core")));
            var replaceDict = new Dictionary<TypeArgumentListSyntax, SeparatedSyntaxList<TypeSyntax>>();
            foreach (var arg in typeArgs)
            {
                var list = Syntax.SeparatedList<TypeSyntax>();
                list = arg.Arguments.Select(a => Syntax.ParseTypeName(a.ToFullString().Replace(_search, _replace))).Aggregate(list, (current, syntax) => current.Add(syntax));
                replaceDict.Add(arg, list);
            }
            return root.ReplaceNodes(replaceDict.Keys.ToList(), (n1, n2) => n1.WithArguments(replaceDict[n1]));
        }

        private CompilationUnitSyntax ReplaceTypeConstraints(CompilationUnitSyntax root)
        {
            var types = root.DescendantNodes().OfType<TypeConstraintSyntax>().Where(tc => tc.Type.ToFullString().StartsWith(_search));
            return root.ReplaceNodes(types,
                                     (n1, n2) =>
                                     n1.WithType(Syntax.ParseTypeName(n1.ToFullString().Replace(_search, _replace))));
        }

        private CompilationUnitSyntax ReplaceBaseLists(CompilationUnitSyntax root)
        {
            var bases = root.DescendantNodes().OfType<BaseListSyntax>();
            var replaceDict = new Dictionary<BaseListSyntax, SeparatedSyntaxList<TypeSyntax>>();
            foreach (var b in bases)
            {
                var list = Syntax.SeparatedList<TypeSyntax>();
                list = b.Types.Select(a => Syntax.ParseTypeName(a.ToFullString().Replace(_search, _replace))).Aggregate(list, (current, syntax) => current.Add(syntax));
                replaceDict.Add(b, list);
            }
            return root.ReplaceNodes(replaceDict.Keys.ToList(), (n1, n2) => n1.WithTypes(replaceDict[n1]));
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
