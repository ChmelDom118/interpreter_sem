namespace InterpreterLib.AST
{
    public class FunctionCallExpression : Expression
    {
        public string Identifier { get; private set; }
        public List<Argument> Arguments { get; private set; }

        public FunctionCallExpression(string identifier, List<Argument> arguments)
        {
            Identifier = identifier;
            Arguments = arguments;
        }
    }
}
