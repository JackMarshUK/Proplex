using System.Collections.Generic;

namespace Proplex.Core.Syntax

{
    public sealed class LiteralExpressionSyntax : ExpressionSyntax
    {
        public SyntaxToken LiteralToken
        {
            get;
        }

        public object Value
        {
            get;
        }


        public LiteralExpressionSyntax(SyntaxToken literalToken) : this(literalToken, literalToken.Value)
        {
        }

        public LiteralExpressionSyntax(SyntaxToken literalToken, object value)
        {
            this.LiteralToken = literalToken;
            this.Value = value;
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
