// See https://github.com/Storm9999/NightLang for more info 
using System;

namespace NightL
{
    public enum SyntaxKind
    {
        NumberTok,
        WhitespaceTok,
        PlusTok,
        MinusTok,
        StarTok,
        SlashTok,
        OpenParanthesisTok,
        CloseParanthesisTok,
        BadTok,
        EndOfFileTok,
        NumberExpression,
        BinaryExpression,
        ParenthesizedExpression
    }
}