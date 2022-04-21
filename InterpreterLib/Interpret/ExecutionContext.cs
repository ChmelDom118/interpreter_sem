namespace InterpreterLib.Interpret
{
    public class ExecutionContext
    {
        public Variables Variables { get; }

        public ExecutionContext(Variables variables)
        {
            Variables = variables;
        }
    }
}
