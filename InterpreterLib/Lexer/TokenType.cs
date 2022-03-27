namespace InterpreterLib.Lexer
{
    public enum TokenType
    {
        Identifier,
        Literal, // literals (true, false, 0.457, 12, "Hello world"):
        Keyword,
        Datatype,

        // other tokens (separators, operators, ...):
        Arrow, Assign, LessThanOrEqualTo, GreaterThanOrEqualTo, LessThan, GreaterThan, EqualsTo, NotEqualsTo, PlusSign, MinusSign, 
        MultiplicationSign, DivisionSign, DecimalSeparator, Semicolon, Colon, Comma, LeftPaprenthesis, RightParenthesis, 
        
        // special tokens:
        Unknown, EOF
    }
}
