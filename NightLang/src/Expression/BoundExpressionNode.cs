using System;

namespace NightL
{
    abstract class BoundExpressionNode : BoundNode
    {
        public abstract Type Type { get; }
    }
}