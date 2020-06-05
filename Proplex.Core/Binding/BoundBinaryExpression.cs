using Proplex.Core.Binding;
using System;

namespace Proplex.Core.Binding
{
    internal sealed class BoundBinaryExpression : BoundExpression
    {
        public BoundBinaryExpression(BoundExpression left, BoundBinaryOperator op, BoundExpression right)
        {
            Left = left;
            Op = op;
            Right = right;
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

        public override Type Type => Op.ResultType;

        /// <inheritdoc />
        public override BoundNodeKind kind => BoundNodeKind.BinaryExpression;
    }
}