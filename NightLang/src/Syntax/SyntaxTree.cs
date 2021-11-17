// See https://github.com/Storm9999/NightLang for more info 
using System;

namespace NightL
{
    class SyntaxTree
    {
        public IReadOnlyList<string> errors { get; }

        public ExpressionSyntax rootex { get; }
        public SyntaxToken end { get; }

        public SyntaxTree(IEnumerable<string> Errors, ExpressionSyntax RootEx, SyntaxToken End)
        {
            errors = Errors.ToArray();
            rootex = RootEx;
            end = End;
        }
    }
}