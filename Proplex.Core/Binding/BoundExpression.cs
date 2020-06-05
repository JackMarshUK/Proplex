using Proplex.Core.Binding;
using System;

namespace Proplex.Core.Binding
{
    internal abstract class BoundExpression : BoundNode
    {
        public abstract Type Type
        {
            get;
        }
    }
}