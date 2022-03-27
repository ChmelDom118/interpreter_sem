namespace InterpreterLib.AST
{
    public class NotEqualsCondition : BinaryRelCondition
    {
        public NotEqualsCondition(Expression left, Expression right) : base(left, right) { }
    }
}
