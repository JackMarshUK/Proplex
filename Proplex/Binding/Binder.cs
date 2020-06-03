//  Proplex

using System;
using System.Collections.Generic;
using Proplex.Errors;
using Proplex.Syntax;

namespace Proplex.Binding
{
    internal sealed class Binder
    {
        private readonly List<string> m_diagnostics = new List<string>();

        public BoundExpression BindExpression(ExpressionSyntax syntax)
        {
            switch(syntax.Kind)
            {
                case SyntaxKind.LiteralExpression: 
                    return BindLiteralExpression((LiteraExpressionSyntax)syntax);
                case SyntaxKind.UnaryExpression:
                    return BindUnaryExpression((UnaryExpressionSyntax)syntax);
                case SyntaxKind.BinaryExpression:
                    return BindBinaryExpression((BinaryExpressionSyntax)syntax);
                default:
                    throw new InvalidSyntaxKindException($"Error 106: Unexpected syntax kind '{syntax.Kind}' in the Binder");
            }
        }

        private static BoundExpression BindLiteralExpression(LiteraExpressionSyntax syntax)
        {
            var value = syntax.Value ?? 0;
            return new BoundLiteralExpression(value);
        }

        private BoundExpression BindUnaryExpression(UnaryExpressionSyntax syntax)
        {

            var boundOperand = BindExpression(syntax.Operand);
            var boundOperatorKind = BoundUnaryOperator.Bind(syntax.OperatorToken.Kind, boundOperand.Type);

            return new BoundUnaryExpression(boundOperatorKind, boundOperand);
        }

        private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax)
        {
            var boundLeft = BindExpression(syntax.Left);
            var boundRight = BindExpression(syntax.Right);
            var boundOperator = BoundBinaryOperator.Bind(syntax.OperatorToken.Kind, boundLeft.Type, boundRight.Type);

            return new BoundBinaryExpression(boundLeft, boundOperator, boundRight);
        }

        public IEnumerable<string> Diagnostics => m_diagnostics;
    }
}



