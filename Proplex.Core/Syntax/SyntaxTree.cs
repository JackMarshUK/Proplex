using System.Collections.Generic;
using System.Linq;
using Proplex.Core.Evaluator;

namespace Proplex.Core.Syntax
{
    public sealed class SyntaxTree
    {
        public IReadOnlyList<Diagnostic> Diagnostics
        {
            get;
        }

        public ExpressionSyntax Root
        {
            get;
        }

        public SyntaxToken EndOfFileToken
        {
            get;
        }

        public SyntaxTree(IEnumerable<Diagnostic> diagnostics, ExpressionSyntax root, SyntaxToken endOfFileToken)
        {
            this.Diagnostics = diagnostics.ToArray();
            this.Root = root;
            this.EndOfFileToken = endOfFileToken;
        }

        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
    }
}
