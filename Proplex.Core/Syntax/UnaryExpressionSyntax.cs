using System.Collections.Generic;

namespace Proplex.Core.Syntax
{
    public sealed class UnaryExpressionSyntax : ExpressionSyntax
    {
        public UnaryExpressionSyntax(SyntaxToken operatorToken, ExpressionSyntax operand)
        {
            this.OperatorToken = operatorToken;
            this.Operand = operand;
        }

        public SyntaxToken OperatorToken
        {
            get;
        }

        public ExpressionSyntax Operand
        {
            get;
        }

        /// <inheritdoc />
        public override SyntaxKind Kind => SyntaxKind.UnaryExpression;

        /// <inheritdoc />
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return this.OperatorToken;
            yield return this.Operand;
        }
    }
}
