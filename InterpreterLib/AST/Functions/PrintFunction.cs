namespace InterpreterLib.AST
{
    public class PrintFunction : Function
    {
        public PrintFunction(string identifier, Parameter parameter)
            : base(identifier, new List<Parameter>() { parameter }, new Block(new List<Statement>() { new PrintStatement(parameter.Identifier) }), null, null) {}
    }
}
