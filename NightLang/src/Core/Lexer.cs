// See https://github.com/Storm9999/NightLang for more info 
using System;

namespace NightL
{
    class Lexer
    {
        private readonly string _text;
        private int pos;
        private List<string> Errors = new List<string>();

        public Lexer(string text)
        {
            _text = text;
        }
        public char CurrentChar
        {
            get
            {
                if (pos >= _text.Length)
                {
                    return '\0';
                }
                return _text[pos];
            }
        }

        public IEnumerable<string> ErrorHandleEnum => Errors;

        private void Next()
        {
            pos += 1;
        }
        public SyntaxToken GetNextToken()
        {
            if (pos >= _text.Length)
            {
                return new SyntaxToken("\0", pos, SyntaxKind.EndOfFileTok, string.Empty);
            }

            if (char.IsDigit(CurrentChar))
            {
                var beginning = pos;

                while (char.IsDigit(CurrentChar))
                {
                    Next();
                }
                var length = pos - beginning;
                var txt = _text.Substring(beginning, length);
                if (!int.TryParse(txt, out var val))
                {
                    Errors.Add($"[NL STANDARD] ERROR: The number {txt} isn't an valid Int32. Please consider fixing the error to run a successful compilation.");
                }
                return new SyntaxToken(txt, beginning, SyntaxKind.NumberTok, val);
            }
            if (char.IsWhiteSpace(CurrentChar))
            {
                var beginning = pos;

                while (char.IsWhiteSpace(CurrentChar))
                {
                    Next();
                }
                var length = pos - beginning;
                var txt = _text.Substring(beginning, length);
                int.TryParse(txt, out var val);
                return new SyntaxToken(txt, beginning, SyntaxKind.WhitespaceTok, val);
            }

            if (CurrentChar == '+')
            {
                return new SyntaxToken("+", pos++, SyntaxKind.PlusTok, null);
            }
            else if (CurrentChar == '-')
            {
                return new SyntaxToken("-", pos++, SyntaxKind.MinusTok, null);
            }
            else if (CurrentChar == '*')
            {
                return new SyntaxToken("*", pos++, SyntaxKind.StarTok, null);
            }
            else if (CurrentChar == '/')
            {
                return new SyntaxToken("/", pos++, SyntaxKind.SlashTok, null);
            }
            else if (CurrentChar == '(')
            {
                return new SyntaxToken("(", pos++, SyntaxKind.OpenParanthesisTok, null);
            }
            else if (CurrentChar == ')')
            {
                return new SyntaxToken(")", pos++, SyntaxKind.CloseParanthesisTok, null);
            }
            Errors.Add($"[NL STANDARD] ERROR: Bad character input => '{CurrentChar}'");
            return new SyntaxToken(_text.Substring(pos - 1, 1), pos++, SyntaxKind.BadTok, string.Empty);
        }
    }
}