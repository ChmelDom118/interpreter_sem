namespace InterpreterLib.AST
{
    public class UnaryExpression : Expression
    {
        protected Expression expression;

        public UnaryExpression(Expression expression)
        {
            this.expression = expression;
        }
    }
}
