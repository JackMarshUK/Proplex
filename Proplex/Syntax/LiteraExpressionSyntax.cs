using System.Collections.Generic;

namespace Proplex.Syntax
{
    public sealed class LiteraExpressionSyntax : ExpressionSyntax
    {
        public SyntaxToken LiteralToken
        {
            get;
        }

        public object Value
        {
            get;
        }


        public LiteraExpressionSyntax(SyntaxToken literalToken) : this(literalToken, literalToken.Value)
        {
        }

        public LiteraExpressionSyntax(SyntaxToken literalToken, object value)
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
