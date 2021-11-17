using System;

namespace NightL
{
    sealed class Binder
    {
        private List<string> Errors = new List<string>();
        public IEnumerable<string> ErrorEnum => Errors;
        public BoundExpressionNode Bind(ExpressionSyntax syntax)
        {
            switch (syntax.Kind)
            {
                case SyntaxKind.NumberExpression:
                {
                    return BindLiteralExpression((NumberSyntax)syntax);
                }
                case SyntaxKind.BinaryExpression:
                {
                    return BindBinaryExpression((BinaryExpressionSyntax)syntax);
                }

                default: 
                {
                    throw new Exception($"[NL STANDARD] ERROR: Unexpected syntax => {syntax.Kind}");
                }
            }
        }

        private BoundExpressionNode BindBinaryExpression(BinaryExpressionSyntax syntax)
        {
            var left = Bind(syntax.Left);
            var right = Bind(syntax.Right);
            var opKind = BindBinaryOperatorKind(syntax.OperatorTok.Kind, left.Type, right.Type);

            if (opKind == null)
            {
                Errors.Add($"[NL STANDARD] ERROR: Binary Operator '{syntax.OperatorTok.Text}' is not defined for Types: {left.Type} and {right.Type}");
                return left;
            }
            return new BoundBinaryExpression(left, opKind.Value, right);
        }

        private BoundBinaryOperatorKind? BindBinaryOperatorKind(SyntaxKind kind, Type leftType, Type rightType)
        {
            if (leftType != typeof(int) || rightType != typeof(int))
              return null;

            switch (kind)
            {
                case SyntaxKind.PlusTok:
                {
                    return BoundBinaryOperatorKind.Addition;
                }
                case SyntaxKind.MinusTok:
                {
                    return BoundBinaryOperatorKind.Subtraction;
                }
                case SyntaxKind.StarTok:
                {
                    return BoundBinaryOperatorKind.Multiplication;
                }
                case SyntaxKind.SlashTok:
                {
                    return BoundBinaryOperatorKind.Division;
                }

                default:
                {
                    throw new Exception($"[NL STANDARD] Unexpected binary operator => {kind}");
                }
            }
        }

        private BoundExpressionNode BindLiteralExpression(NumberSyntax syntax)
        {
            var val = syntax.Token.Val as int? ?? 0;
            return new BoundLiteralExpression(val);

        }
    }
}