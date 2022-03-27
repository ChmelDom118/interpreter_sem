namespace InterpreterLib.AST
{
    public class GreaterEqualCondition : BinaryRelCondition
    {
        public GreaterEqualCondition(Expression left, Expression right) : base(left, right) { }
    }
}