namespace InterpreterLib.AST
{
    internal class IntegerLiteralExpression : LiteralExpression<int>
    {
        public IntegerLiteralExpression(int value) : base(value) { }
    }
}
