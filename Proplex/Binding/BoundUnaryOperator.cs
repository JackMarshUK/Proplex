using System;
using Proplex.Syntax;

namespace Proplex.Binding
{
    internal sealed class BoundUnaryOperator
    {
        private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType)
            :
            this(syntaxKind, kind, operandType, operandType)
        {
        }

        private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType, Type resultType)
        {
            this.SyntaxKind = syntaxKind;
            this.Kind = kind;
            this.OperandType = operandType;
            this.ResultType = resultType;
        }

        public SyntaxKind SyntaxKind
        {
            get;
        }

        public BoundUnaryOperatorKind Kind
        {
            get;

        }

        public Type OperandType
        {
            get;

        }

        public Type ResultType
        {
            get;

        }

        private static BoundUnaryOperator[] m_operators =
        {
            new BoundUnaryOperator(SyntaxKind.BangToken, BoundUnaryOperatorKind.LogicalNegation, typeof(bool)),
            new BoundUnaryOperator(SyntaxKind.PlusToken, BoundUnaryOperatorKind.Identity, typeof(int)),
            new BoundUnaryOperator(SyntaxKind.MinusToken, BoundUnaryOperatorKind.Negation, typeof(int)),
        };

        public static BoundUnaryOperator Bind(SyntaxKind syntaxKind, Type operandType)
        {
            foreach(var op in m_operators)
            {
                if(op.SyntaxKind == syntaxKind && op.OperandType == operandType)
                    return op;
            }

            return null;
        }
    }
}