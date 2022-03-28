namespace InterpreterLib.AST
{
    public class AssignStatement : Statement
    {
        public DataType? DataType { get; private set; }
        public string Identifier { get; private set; }
        public Condition? Condition { get; private set; }
        public Expression? Expression { get; private set; }

        public AssignStatement(DataType? dataType, string idenifier, Condition? condition = null, Expression? expression = null)
        {
            DataType = dataType;
            Identifier = idenifier;
            Condition = condition;
            Expression = expression;
        }
    }
}
