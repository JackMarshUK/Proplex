using System;

namespace Proplex.Binding
{
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
        public override Type Type => this.Operand.Type;

        /// <inheritdoc />
        public override BoundNodeKind kind => BoundNodeKind.UnaryExpression;
    }
}