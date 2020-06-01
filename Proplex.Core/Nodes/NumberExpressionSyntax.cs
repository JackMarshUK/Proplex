using System.Collections.Generic;

namespace Proplex.Core.Nodes
{
    public sealed class NumberExpressionSyntax : ExpressionSyntax
    {
        public SyntaxToken NumberToken
        {
            get;
        }

        public NumberExpressionSyntax(SyntaxToken numberToken)
        {
            this.NumberToken = numberToken;
        }
        /// <inheritdoc />
        public override SyntaxKind Kind => SyntaxKind.NumberExpression;

        /// <inheritdoc />
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return this.NumberToken;
        }
    }
}
