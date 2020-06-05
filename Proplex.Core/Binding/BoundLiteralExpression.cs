using Proplex.Core.Binding;
using System;

namespace Proplex.Core.Binding
{
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
        public override Type Type => this.Value.GetType();

        /// <inheritdoc />
        public override BoundNodeKind kind => BoundNodeKind.LiteralExpression;
    }
}