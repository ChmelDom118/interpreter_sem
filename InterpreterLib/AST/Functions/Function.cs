namespace InterpreterLib.AST
{
    public class Function
    {
        public string Identifier { get; }
        public List<Parameter> Parameters { get; }
        public Block Block { get; }
        public DataType? DataType { get; protected set; }
        public Argument? ReturnArgument { get; }

        public Function(string identifier, List<Parameter> parameters, Block block, DataType? dataType = null, Argument? returnArgument = null)
        {
            Identifier = identifier;
            Parameters = parameters;
            Block = block;
            DataType = dataType;
            ReturnArgument = returnArgument;
        }
    }
}
