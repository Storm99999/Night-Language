// See https://github.com/Storm9999/NightLang for more info 
using System;

namespace NightL
{
    class Parser
    {
        private readonly SyntaxToken[] syntaxTokens;
        private int pos;
        private List<string> Errors = new List<string>();
        public IEnumerable<string> ErrorHandleEnum => Errors;
        public Parser(string text)
        {
            List<SyntaxToken> tokens = new List<SyntaxToken>();
            Lexer lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.GetNextToken();


                if (token.Kind != SyntaxKind.WhitespaceTok && token.Kind != SyntaxKind.BadTok)
                {
                    tokens.Add(token);
                }

            } while (token.Kind != SyntaxKind.EndOfFileTok);
            syntaxTokens = tokens.ToArray();
            Errors.AddRange(lexer.ErrorHandleEnum);
        }

        private SyntaxToken NextToken()
        {
            var curr = Current;
            pos++;
            return curr;
        }
        
        private ExpressionSyntax ParseExpression(int parentPrec)
        {
            var left = ParsePrimaryExpression();

            while (true)
            {
                 int prec = GetBinaryOperatorPrecedance(Current.Kind);
                 if (prec == 0 || prec <= parentPrec)
                 {
                     break;
                 }
                 else
                 {
                     var opToken = NextToken();
                     var right = ParseExpression(prec);
                     left = new BinaryExpressionSyntax(left, opToken, right);
                 }
            }
            return left;
        }
        

        private static int GetBinaryOperatorPrecedance(SyntaxKind syntax)
        {
            switch (syntax)
            {
                case SyntaxKind.PlusTok:
                case SyntaxKind.MinusTok:
                  return 1;
                case SyntaxKind.SlashTok:
                case SyntaxKind.StarTok:
                  return 2;


                default:
                 return 0;
            }
        }

        private SyntaxToken Match(SyntaxKind syntax)
        {
            if (Current.Kind == syntax)
            {
                return NextToken();
            }
            else
            {
                Errors.Add($"[NL STANDARD] ERROR: Unexpected Token => '{Current.Kind}', please consider changing to {syntax}");
                return new SyntaxToken(null, Current.Position, syntax, null);
            }
        }

        public SyntaxTree Parse()
        {
            var Express = ParseEx();
            var eofTok = Match(SyntaxKind.EndOfFileTok);
            return new SyntaxTree(ErrorHandleEnum, Express, eofTok);
        }

        private ExpressionSyntax ParseEx()
        {
            var left = ParsePrimaryExpression();

            while (Current.Kind == SyntaxKind.PlusTok || Current.Kind == SyntaxKind.MinusTok || Current.Kind == SyntaxKind.SlashTok || Current.Kind == SyntaxKind.StarTok || Current.Kind == SyntaxKind.OpenParanthesisTok || Current.Kind == SyntaxKind.CloseParanthesisTok)
            {
                var opToken = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, opToken, right);
            }
            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (Current.Kind == SyntaxKind.OpenParanthesisTok)
            {
                var left = NextToken();
                var express = ParseEx();
                var right = Match(SyntaxKind.CloseParanthesisTok);
                return new ParentExpressionSyntax(left, express, right);
            }
            var numbTok = Match(SyntaxKind.NumberTok);
            return new NumberSyntax(numbTok);
        }

        private SyntaxToken Peek(int offset)
        {
            var index = pos + offset;
            if (index >= syntaxTokens.Length)
            {
                return syntaxTokens[syntaxTokens.Length - 1];
            }
            return syntaxTokens[index];
        }

        private SyntaxToken Current => Peek(0);
    }
}