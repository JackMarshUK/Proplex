using System.Collections.Generic;

namespace Proplex.Core.Nodes
{
    public sealed class LiteraExpressionSyntax : ExpressionSyntax
    {
        public SyntaxToken NumberToken
        {
            get;
        }

        public LiteraExpressionSyntax(SyntaxToken numberToken)
        {
            this.NumberToken = numberToken;
        }
        /// <inheritdoc />
        public override SyntaxKind Kind => SyntaxKind.LiteralExpression;

        /// <inheritdoc />
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return this.NumberToken;
        }
    }
}
