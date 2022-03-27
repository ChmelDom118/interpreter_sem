namespace InterpreterLib.AST
{
    public class AssignStatement : Statement
    {
        public DataType? DataType { get; private set; }
        public string Identifier { get; private set; }

        public AssignStatement(DataType? dataType, string idenifier, Condition condition)
        {
            DataType = dataType;
            Identifier = idenifier;
        }

        public AssignStatement(DataType? dataType, string idenifier, Expression expression)
        {
            DataType = dataType;
            Identifier = idenifier;
        }
    }
}
