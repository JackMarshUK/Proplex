using System;
using System.Collections.Generic;
using System.Text;

namespace Proplex.Lexer.Nodes
{
    public sealed class BinaryExpressionSyntax : ExpressionSyntax
    {
        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
        {
            this.Left = left;
            this.OperatorToken = operatorToken;
            this.Right = right;
        }

        public ExpressionSyntax Left
        {
            get;
        }

        public SyntaxToken OperatorToken
        {
            get;
        }

        public ExpressionSyntax Right
        {
            get;
        }

        /// <inheritdoc />
        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;

        /// <inheritdoc />
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }
}
