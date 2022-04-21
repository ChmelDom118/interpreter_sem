namespace InterpreterLib.AST
{
    public abstract class Var
    {
        public string Identifier { get; }
        public DataType DataType { get; }

        public Var(string identifier, DataType dataType)
        {
            Identifier = identifier;
            DataType = dataType;
        }
    }
}
