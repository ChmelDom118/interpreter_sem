namespace InterpreterLib.AST
{
    public class LiteralCondition : Condition
    {
        public bool Value { get; private set; }

        public LiteralCondition(bool value)
        {
            Value = value;
        }
    }
}
