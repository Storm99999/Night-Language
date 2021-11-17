// See https://github.com/Storm9999/NightLang for more info 
using System;

namespace NightL
{
    sealed class ParentExpressionSyntax : ExpressionSyntax
    {
        public ParentExpressionSyntax(SyntaxToken openTok, ExpressionSyntax expression, SyntaxToken close)
        {
            OpenTok = openTok;
            Expression = expression;
            Close = close;
        }

        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;
        public SyntaxToken OpenTok { get; }
        public ExpressionSyntax Expression { get; }
        public SyntaxToken Close { get; }

        public override IEnumerable<SyntaxNode> GetChilds()
        {
            yield return OpenTok;
            yield return Expression;
            yield return Close;
        }
    }
}