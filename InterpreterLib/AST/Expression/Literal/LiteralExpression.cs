namespace InterpreterLib.AST
{
    public abstract class LiteralExpression<T>
    {
        public T Value { get; protected set; }

        public LiteralExpression(T value)
        {
            Value = value;
        }
    }
}
