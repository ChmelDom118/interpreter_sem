namespace InterpreterLib.AST
{
    public class LessEqualCondition : BinaryRelCondition
    {
        public LessEqualCondition(Expression left, Expression right) : base(left, right) { }
    }
}