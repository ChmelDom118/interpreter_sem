namespace InterpreterLib.AST
{
    public class IfStatement : Statement
    {
        public Condition Condition { get; private set; }
        public Block Block { get; private set; }
        public List<Tuple<Condition, Block>> ElseIfBranches { get; private set; }
        public Block? ElseBranch { get; private set; }

        public IfStatement(Condition condition, Block block, List<Tuple<Condition, Block>> elseIfBranches, Block? elseBranch)
        {
            Condition = condition;
            Block = block;
            ElseIfBranches = elseIfBranches;
            ElseBranch = elseBranch;
        }
    }
}
