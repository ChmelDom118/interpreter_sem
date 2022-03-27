namespace InterpreterLib.AST
{
    internal class DoubleLiteralExpression : LiteralExpression<double>
    {
        public DoubleLiteralExpression(double value) : base(value) { }
    }
}
