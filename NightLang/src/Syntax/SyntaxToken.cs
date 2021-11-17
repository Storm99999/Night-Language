// See https://github.com/Storm9999/NightLang for more info 
using System;

namespace NightL
{
    class SyntaxToken : SyntaxNode
    {
        public SyntaxToken(string text, int position, SyntaxKind kind, object val)
        {
            Text = text;
            Position = position;
            Kind = kind;
            Val = val;
        }

        public string Text { get; }
        public int Position { get; }
        public override SyntaxKind Kind { get; }
        public object Val { get; }

        public override IEnumerable<SyntaxNode> GetChilds()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}