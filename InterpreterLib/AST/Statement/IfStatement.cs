namespace InterpreterLib.AST
{
    public class IfStatement : Statement
    {
        public Condition Condition { get; private set; }
        public Block Block { get; private set; }
        public IfStatement ElseStatement { get; private set; }

        public IfStatement(Condition condition, Block block, IfStatement elseStatement)
        {
            Condition = condition;
            Block = block;
            ElseStatement = elseStatement;
        }
    }
}
