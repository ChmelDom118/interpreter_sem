namespace InterpreterLib.AST
{
    public class Parameter
    {
        public DataType DataType { get; private set; }
        public string Identifier { get; private set; }

        public Parameter(DataType dataType, string identifier)
        {
            DataType = dataType;
            Identifier = identifier;
        }
    }
}
