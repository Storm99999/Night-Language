// See https://github.com/Storm9999/NightLang for more info 
using System;

namespace NightL
{
    sealed class BinaryExpressionSyntax : ExpressionSyntax
    {
        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorTok, ExpressionSyntax right)
        {
            Left = left;
            OperatorTok = operatorTok;
            Right = right;
        }
        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;

        public ExpressionSyntax Left { get; }
        public SyntaxToken OperatorTok { get; }
        public ExpressionSyntax Right { get; }

        public override IEnumerable<SyntaxNode> GetChilds()
        {
            yield return Left;
            yield return OperatorTok;
            yield return Right;
        }
    }
}