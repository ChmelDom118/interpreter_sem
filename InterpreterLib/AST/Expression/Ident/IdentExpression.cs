namespace InterpreterLib.AST
{
    public class IdentExpression : Expression
    {
        public string Identifier { get; private set; }

        public IdentExpression(string identifier)
        {
            Identifier = identifier;
        }
    }
}
