//  Proplex

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Proplex.Core.Syntax;
using Proplex.Errors;

namespace Proplex.Core.Binding
{
    internal enum BoundNodeKind
    {
        UnaryExpression,
        LiteralExpression,
        BinaryExpression
    }

    internal abstract class BoundNode
    {
        public abstract BoundNodeKind kind
        {
            get;
        }
    }

    internal abstract class BoundExpression : BoundNode
    {
        public abstract Type Type
        {
            get;
        }
    }

    internal enum BoundUnaryOperatorKind
    {
        Identity,
        Negation
    }

    internal enum BoundBinaryOperatorKind
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    internal sealed class BoundLiteralExpression : BoundExpression
    {
        public BoundLiteralExpression(object value)
        {
            this.Value = value;
        }

        public object Value
        {
            get;
        }

        /// <inheritdoc />
        public override Type Type => Value.GetType();

        /// <inheritdoc />
        public override BoundNodeKind kind => BoundNodeKind.LiteralExpression;
    }

    internal sealed class BoundBinaryExpression : BoundExpression
    {
        public BoundBinaryExpression(BoundExpression left, BoundBinaryOperatorKind operatorKind, BoundExpression right)
        {
            this.Left = left;
            this.OperatorKind = operatorKind;
            this.Right = right;
        }

        public BoundExpression Left
        {
            get;
        }

        public BoundBinaryOperatorKind OperatorKind
        {
            get;
        }

        public BoundExpression Right
        {
            get;
        }

        public override Type Type => Left.Type;

        /// <inheritdoc />
        public override BoundNodeKind kind => BoundNodeKind.BinaryExpression;
    }

    internal sealed class BoundUnaryExpression : BoundExpression
    {
        public BoundUnaryExpression(BoundUnaryOperatorKind operatorKind, BoundExpression operand)
        {
            this.OperatorKind = operatorKind;
            this.Operand = operand;
        }

        public BoundUnaryOperatorKind OperatorKind
        {
            get;
        }

        public BoundExpression Operand
        {
            get;
        }

        /// <inheritdoc />
        public override Type Type => Operand.Type;

        /// <inheritdoc />
        public override BoundNodeKind kind => BoundNodeKind.UnaryExpression;
    }

    internal sealed class Binder
    {
        private readonly List<string> m_diagnostics = new List<string>();

        public IEnumerable<string> Diagnostics => m_diagnostics;

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
            var value = syntax.LiteralToken.Value as int? ?? 0;
            return new BoundLiteralExpression(value);
        }

        private BoundExpression BindUnaryExpression(UnaryExpressionSyntax syntax)
        {
            var boundOperand = BindExpression(syntax.Operand);
            var boundOperatorKind = BindUnaryOperatorKind(syntax.OperatorToken.Kind, boundOperand.Type);

            return new BoundUnaryExpression(boundOperatorKind, boundOperand);
        }



        private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax)
        {
            var boundLeft = BindExpression(syntax.Left);
            var boundRight = BindExpression(syntax.Right);
            var boundOperator = BindBinaryOperatorKind(syntax.OperatorToken.Kind, boundLeft.Type, boundRight.Type);

            return new BoundBinaryExpression(boundLeft, boundOperator, boundRight);
        }

        private static BoundUnaryOperatorKind BindUnaryOperatorKind(SyntaxKind kind, Type operandType)
        {
            if(operandType != typeof(int))
                throw new InvalidTypeException($"Error 107: Operand type of {operandType} is invalid in BindUnaryOperatorKind");

            switch(kind)
            {
                case SyntaxKind.PlusToken:
                    return BoundUnaryOperatorKind.Identity;
                case SyntaxKind.MinusToken:
                    return BoundUnaryOperatorKind.Negation;
                default: 
                    throw new InvalidSyntaxKindException($"Error 106: Unexpected unary operator '{kind}'in BindUnaryOperatorKind");
            }

        }

        private static BoundBinaryOperatorKind BindBinaryOperatorKind(SyntaxKind kind, Type leftType, Type rightType)
        {
            if(leftType != typeof(int) || rightType != typeof(int))
                throw new InvalidTypeException($"Error 107: left type of {leftType} or right type of {rightType} is invalid in BindBinaryOperatorKind");

            switch(kind)
            {
                case SyntaxKind.PlusToken:
                    return BoundBinaryOperatorKind.Addition;
                case SyntaxKind.MinusToken:
                    return BoundBinaryOperatorKind.Subtraction;
                case SyntaxKind.StarToken:
                    return BoundBinaryOperatorKind.Multiplication;
                case SyntaxKind.SlashToken:
                    return BoundBinaryOperatorKind.Division;
                default:
                    throw new InvalidSyntaxKindException($"Error 106: Unexpected binary operator '{kind}'in BindBinaryOperatorKind");
            }

        }
    }
}



