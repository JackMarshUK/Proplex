using System;

namespace Proplex.Binding
{
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

        public override Type Type => this.Left.Type;

        /// <inheritdoc />
        public override BoundNodeKind kind => BoundNodeKind.BinaryExpression;
    }
}