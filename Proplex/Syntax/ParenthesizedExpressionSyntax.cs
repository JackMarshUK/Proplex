using System.Collections.Generic;

namespace Proplex.Syntax
{
    public sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        public SyntaxToken OpenParenthesisToken
        {
            get;
        }

        public ExpressionSyntax Expression
        {
            get;
        }

        public SyntaxToken ClosedParenthesisToken
        {
            get;
        }

        public ParenthesizedExpressionSyntax(SyntaxToken openParenthesisToken, ExpressionSyntax expression, SyntaxToken closedParenthesisToken)
        {
            this.OpenParenthesisToken = openParenthesisToken;
            this.Expression = expression;
            this.ClosedParenthesisToken = closedParenthesisToken;
        }

        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return this.OpenParenthesisToken;
            yield return this.Expression;
            yield return this.ClosedParenthesisToken;
        }
    }
}
