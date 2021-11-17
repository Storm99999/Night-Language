// See https://github.com/Storm9999/NightLang for more info 
using System;

namespace NightL
{
    sealed class NumberSyntax : ExpressionSyntax
    {
        public NumberSyntax(SyntaxToken token)
        {
            Token = token;
        }

        public override SyntaxKind Kind => SyntaxKind.NumberExpression;

        public SyntaxToken Token { get; }

        public override IEnumerable<SyntaxNode> GetChilds()
        {
            yield return Token;
        }
    }
}