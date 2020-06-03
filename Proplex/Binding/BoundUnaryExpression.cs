using System;

namespace Proplex.Binding
{
    internal sealed class BoundUnaryExpression : BoundExpression
    {
        public BoundUnaryExpression(BoundUnaryOperator op, BoundExpression operand)
        {
            this.Op = op;
            this.Operand = operand;
        }

        public BoundUnaryOperator Op
        {
            get;
        }

        public BoundExpression Operand
        {
            get;
        }

        /// <inheritdoc />
        public override Type Type => this.Op.ResultType;

        /// <inheritdoc />
        public override BoundNodeKind kind => BoundNodeKind.UnaryExpression;
    }
}