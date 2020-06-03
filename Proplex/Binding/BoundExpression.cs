using System;

namespace Proplex.Binding
{
    internal abstract class BoundExpression : BoundNode
    {
        public abstract Type Type
        {
            get;
        }
    }
}