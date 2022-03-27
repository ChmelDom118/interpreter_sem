namespace InterpreterLib.AST
{
    public class EqualsCondition : BinaryRelCondition
    {
        public EqualsCondition(Expression left, Expression right) : base(left, right) { }
    }
}
