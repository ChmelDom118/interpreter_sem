namespace InterpreterLib.AST
{
    public class Multiply : BinaryExpression
    {
        public Multiply(Expression left, Expression right) : base(left, right) { }
    }
}
