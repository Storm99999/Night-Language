// See https://github.com/Storm9999/NightLang for more info 
using System;

namespace NightL
{
    class Evaluator
    {
        public Evaluator(BoundExpressionNode rootExpress)
        {
            RootExpress = rootExpress;
        }

        private readonly BoundExpressionNode RootExpress;

        // This file contains all the methods for results. If you want to swap things then be careful. The compiler might break here.

        public int Evaluate()
        {
            return EvaluateExpression(RootExpress);
        }

        private int EvaluateExpression(BoundExpressionNode rootExpress)
        {
            if (rootExpress is BoundLiteralExpression ble)
            {
                return (int)ble.Val;
            }

            if (rootExpress is BoundBinaryExpression bes)
            {
                var left = EvaluateExpression(bes.Left); //bes.Left;
                var right = EvaluateExpression(bes.Right); //bes.Right;
                
                if (bes.BinaryOperator == BoundBinaryOperatorKind.Addition)
                {
                    return left + right;
                }
                if (bes.BinaryOperator == BoundBinaryOperatorKind.Subtraction)
                {
                    return left - right;
                }
                if (bes.BinaryOperator == BoundBinaryOperatorKind.Division)
                {
                    return left / right;
                }
                if (bes.BinaryOperator == BoundBinaryOperatorKind.Multiplication)
                {
                    return left * right;
                }
            }
            throw new Exception($"[NL STANDARD] ERROR: Unexpected node => {rootExpress.NodeKind}");

            
        }
    }
}