namespace InterpreterLib.AST
{
    public class Minus : BinaryExpression
    {
        public Minus(Expression left, Expression right) : base(left, right) { }
    }
}
