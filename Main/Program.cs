using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4Test;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var stream = File.OpenRead(@"D:\GIT\test\t\src\main\java\a.Tars"))
            {
                var lexer = new GammerLexer(new AntlrInputStream(stream));
                var parser = new GammerParser(new CommonTokenStream(lexer));
                var visitor = new GammerVisitor();
                var a = visitor.Visit(parser.moduleDefinition());
                File.WriteAllText(@"D:\GIT\test\t\src\main\java\a.cs", a.ToFullString());
                //var tree = parser.moduleDefinition();
                //Console.WriteLine(tree.ToStringTree(parser));
            }
            
        }
    }

    public class GammerVisitor : GammerBaseVisitor<CSharpSyntaxNode>
    {
        Dictionary<string, string> Pairs = new Dictionary<string, string>();

        public override CSharpSyntaxNode VisitModuleDefinition([NotNull] GammerParser.ModuleDefinitionContext context)
        {
            var namespaces = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName(context.moduleName().GetText())
                .WithLeadingTrivia(SyntaxFactory.TriviaList(SyntaxFactory.SyntaxTrivia(SyntaxKind.WhitespaceTrivia, " ")))
                .WithTrailingTrivia(SyntaxFactory.TriviaList(SyntaxFactory.SyntaxTrivia(SyntaxKind.EndOfLineTrivia, "\n")))
                );
            namespaces = namespaces.AddMembers(SyntaxFactory.ClassDeclaration(" Program").AddMembers(SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName("System.Windows.Forms.Timer"), "Ticker")
                                    .AddAccessorListAccessors(
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
                                    .WithInitializer(SyntaxFactory.EqualsValueClause(SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(2))))
                                    .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                                    ,
                            SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), "Main")
                                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                                    .WithBody(SyntaxFactory.Block())));
            return SyntaxFactory.CompilationUnit().AddMembers(namespaces);
        }
    }
}
