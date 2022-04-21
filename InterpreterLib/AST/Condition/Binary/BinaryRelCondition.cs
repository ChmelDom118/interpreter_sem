namespace InterpreterLib.AST
{
    public abstract class BinaryRelCondition : Condition
    {
        protected Expression LeftExpression;
        protected Expression RightExpression;

        public BinaryRelCondition(Expression left, Expression right)
        {
            LeftExpression = left;
            RightExpression = right;
        }
    }
}
