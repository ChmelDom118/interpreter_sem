namespace InterpreterLib.AST
{
    public class Plus : BinaryExpression
    {
        public Plus(Expression left, Expression right) : base(left, right) { }
    }
}
