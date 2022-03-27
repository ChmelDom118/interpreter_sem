namespace InterpreterLib.AST
{
    public class IdentCondition : Condition
    {
        public string Identifier { get; private set; }
        
        public IdentCondition(string identifier)
        {
            Identifier = identifier;
        }
    }
}
