// See https://github.com/Storm9999/NightLang for more info 
using System;

namespace NightL
{
    sealed class BoundLiteralExpression : BoundExpressionNode
    {
        public BoundLiteralExpression(object val)
        {
            Val = val;
        }

        public override Type Type => Val.GetType();

        public override BoundNodeKind NodeKind => BoundNodeKind.LiteralExpression;

        public object Val { get; }
    }
}