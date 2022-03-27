namespace InterpreterLib.AST
{
    public class Function
    {
        public string Identifier { get; private set; }
        public List<Parameter> Parameters { get; private set; }
        public Block Block { get; private set; }
        public DataType? DataType { get; private set; }
        public Argument? ReturnArgument { get; private set; }

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
