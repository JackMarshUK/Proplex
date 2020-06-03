using System;

namespace Proplex.Binding
{
    internal sealed class BoundBinaryExpression : BoundExpression
    {
        public BoundBinaryExpression(BoundExpression left, BoundBinaryOperator op, BoundExpression right)
        {
            this.Left = left;
            this.Op = op;
            this.Right = right;
        }

        public BoundExpression Left
        {
            get;
        }

        public BoundBinaryOperator Op
        {
            get;
        }

        public BoundExpression Right
        {
            get;
        }

        public override Type Type => this.Op.ResultType;

        /// <inheritdoc />
        public override BoundNodeKind kind => BoundNodeKind.BinaryExpression;
    }
}