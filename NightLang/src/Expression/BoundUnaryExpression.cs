// See https://github.com/Storm9999/NightLang for more info 
using System;

namespace NightL
{
    sealed class BoundUnaryExpression : BoundExpressionNode
    {
        public BoundUnaryExpression(BoundUnaryOperatorKind kind, BoundExpressionNode bound)
        {
            Kind = kind;
            Bound = bound;
        }

        public override Type Type => Bound.Type;

        public override BoundNodeKind NodeKind => BoundNodeKind.UnaryExpression;

        public BoundUnaryOperatorKind Kind { get; }
        public BoundExpressionNode Bound { get; }
    }
}