namespace InterpreterLib.AST
{
    public abstract class BinaryExpression : Expression
    {
        protected Expression LeftExpression;
        protected Expression RightExpression;

        public BinaryExpression(Expression left, Expression right)
        {
            LeftExpression = left;
            RightExpression = right;
        }
    }
}
