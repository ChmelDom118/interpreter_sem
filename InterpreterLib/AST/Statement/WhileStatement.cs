namespace InterpreterLib.AST
{
    public class WhileStatement : Statement
    {
        public Condition Condition { get; private set; }
        public Block Block { get; private set; }

        public WhileStatement(Condition condition, Block block)
        {
            Condition = condition;
            Block = block;
        }
    }
}
