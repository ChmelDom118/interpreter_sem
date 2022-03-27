namespace InterpreterLib.AST
{
    public class BoolLiteralExpression : LiteralExpression<bool>
    {
        public BoolLiteralExpression(bool value) : base(value) { }
    }
}
