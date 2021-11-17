// See https://github.com/Storm9999/NightLang for more info 
using System;

namespace NightL
{
    sealed class BoundBinaryExpression : BoundExpressionNode
    {
        public BoundBinaryExpression(BoundExpressionNode left, BoundBinaryOperatorKind binaryOperator, BoundExpressionNode right)
        {
            Left = left;
            BinaryOperator = binaryOperator;
            Right = right;
        }

        public override Type Type => Left.Type;

        public override BoundNodeKind NodeKind => BoundNodeKind.UnaryExpression;

        public BoundExpressionNode Left { get; }
        public BoundBinaryOperatorKind BinaryOperator { get; }
        public BoundExpressionNode Right { get; }
    }
}