namespace InterpreterLib.AST
{
    public abstract class LiteralExpression<T> : Expression
    {
        public T Value { get; protected set; }

        public LiteralExpression(T value)
        {
            Value = value;
        }
    }
}
