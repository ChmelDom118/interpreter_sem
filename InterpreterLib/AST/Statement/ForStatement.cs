namespace InterpreterLib.AST
{
    public class ForStatement : Statement
    {
        public string Identifier { get; private set; }
        public Expression InitExpression { get; private set; }
        public Condition Condition { get; private set; }
        public Expression Expression { get; set; }
        public Block Block { get; private set; }

        public ForStatement(string identifier, Expression initExpression, Condition condition, Expression expression, Block block)
        {
            Identifier = identifier;
            InitExpression = initExpression;
            Condition = condition;
            Expression = expression;
            Block = block;
        }
    }
}
