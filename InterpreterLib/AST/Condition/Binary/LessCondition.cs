namespace InterpreterLib.AST
{
    public class LessCondition : BinaryRelCondition
    {
        public LessCondition(Expression left, Expression right) : base(left, right) { }
    }
}