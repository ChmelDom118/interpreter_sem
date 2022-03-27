namespace InterpreterLib.AST
{
    public class GreaterCondition : BinaryRelCondition
    {
        public GreaterCondition(Expression left, Expression right) : base(left, right) { }
    }
}