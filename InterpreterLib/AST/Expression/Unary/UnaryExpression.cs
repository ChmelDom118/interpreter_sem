namespace InterpreterLib.AST
{
    public class UnaryExpression
    {
        protected Expression expression;

        public UnaryExpression(Expression expression)
        {
            this.expression = expression;
        }
    }
}
