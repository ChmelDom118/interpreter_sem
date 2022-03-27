namespace InterpreterLib.AST
{
    public class Argument
    {
        public Condition? Condition { get; private set; }
        public Expression? Expression { get; private set; }

        public Argument(Condition condition)
        {
            Condition = condition;
        }

        public Argument(Expression expression)
        {
            Expression = expression;
        }
    }
}
