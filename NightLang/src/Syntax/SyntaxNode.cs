// See https://github.com/Storm9999/NightLang for more info 
using System;

namespace NightL
{
    abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }
        public abstract IEnumerable<SyntaxNode> GetChilds();
    }
}