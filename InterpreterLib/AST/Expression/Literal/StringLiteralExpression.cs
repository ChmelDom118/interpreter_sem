namespace InterpreterLib.AST
{
    public class StringLiteralExpression : LiteralExpression<string>
    {
        public StringLiteralExpression(string value) : base(value) { }
    }
}
