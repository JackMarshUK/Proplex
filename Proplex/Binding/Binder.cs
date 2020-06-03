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

        private static BoundUnaryOperatorKind BindUnaryOperatorKind(SyntaxKind kind, Type operandType)
        {
            if(operandType == typeof(int)) {
                return kind switch
                {
                    SyntaxKind.PlusToken => BoundUnaryOperatorKind.Identity,
                    SyntaxKind.MinusToken => BoundUnaryOperatorKind.Negation,
                    _ => throw new InvalidSyntaxKindException($"Error 106: Unexpected unary operator '{kind}'in BindUnaryOperatorKind"),
                };
            }

            if(operandType != typeof(bool))
            {
                throw new InvalidTypeException($"Error 107: Operand type of {operandType} is invalid in BindUnaryOperatorKind");
            }

            return kind switch
            {
                SyntaxKind.BangToken => BoundUnaryOperatorKind.LogicalNegation,
                _ => throw new InvalidTypeException($"Error 107: Operand type of {operandType} is invalid in BindUnaryOperatorKind"),
            };
        }

        private static BoundBinaryOperatorKind BindBinaryOperatorKind(SyntaxKind kind, Type leftType, Type rightType)
        {
            if(leftType == typeof(int) && rightType == typeof(int))
            {
                return kind switch
                {
                    SyntaxKind.PlusToken => BoundBinaryOperatorKind.Addition,
                    SyntaxKind.MinusToken => BoundBinaryOperatorKind.Subtraction,
                    SyntaxKind.StarToken => BoundBinaryOperatorKind.Multiplication,
                    SyntaxKind.SlashToken => BoundBinaryOperatorKind.Division,
                    _ => throw new InvalidSyntaxKindException($"Error 106: Unexpected binary operator '{kind}'in BindBinaryOperatorKind"),
                };
            }

            if(leftType != typeof(bool) || rightType != typeof(bool))
            {
                throw new InvalidTypeException($"Error 107: left type of {leftType} or right type of {rightType} is invalid in BindBinaryOperatorKind");
            }

            return kind switch
            {
                SyntaxKind.AmpersandAmpersandToken => BoundBinaryOperatorKind.LogicalAnd,
                SyntaxKind.PipePipeToken => BoundBinaryOperatorKind.LogicalOr,
                _ => throw new InvalidTypeException($"Error 107: left type of {leftType} or right type of {rightType} is invalid in BindBinaryOperatorKind")
            };
        }

        public IEnumerable<string> Diagnostics => m_diagnostics;
    }
}



