using System.Collections.Generic;

namespace Proplex.Core.Syntax
{
    public sealed class LiteraExpressionSyntax : ExpressionSyntax
    {
        public SyntaxToken LiteralToken
        {
            get;
        }

        public LiteraExpressionSyntax(SyntaxToken literalToken)
        {
            this.LiteralToken = literalToken;
        }
        /// <inheritdoc />
        public override SyntaxKind Kind => SyntaxKind.LiteralExpression;

        /// <inheritdoc />
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return this.LiteralToken;
        }
    }
}
