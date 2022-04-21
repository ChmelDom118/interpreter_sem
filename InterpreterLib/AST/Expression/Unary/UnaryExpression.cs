namespace InterpreterLib.AST
{
    public abstract class UnaryExpression : Expression
    {
        protected Expression Expression;

        public UnaryExpression(Expression expression)
        {
            Expression = expression;
        }
    }
}
