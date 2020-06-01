using System;
using System.Collections.Generic;
using System.Text;

namespace Proplex.Core.Nodes
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
