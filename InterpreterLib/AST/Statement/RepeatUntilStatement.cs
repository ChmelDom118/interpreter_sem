namespace InterpreterLib.AST
{
    public class RepeatUntilStatement : Statement
    {
        public Condition Condition { get; private set; }
        public Block Block { get; private set; }

        public RepeatUntilStatement(Condition condition, Block block)
        {
            Condition = condition;
            Block = block;
        }
    }
}
