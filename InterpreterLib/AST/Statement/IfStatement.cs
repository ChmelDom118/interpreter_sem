using InterpreterLib.Interpret;

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

        public override void Execute(Interpreter interpret)
        {
            if (Condition.Evaluate(interpret) is VarBool ifConditionResult && ifConditionResult.Value == true)
            {
                foreach (Statement statement in Block.Statements)
                {
                    statement.Execute(interpret);
                }
            }
            else
            {
                foreach(Tuple<Condition, Block> tuple in ElseIfBranches)
                {
                    if (tuple.Item1.Evaluate(interpret) is VarBool elseIfConditionResult && elseIfConditionResult.Value == true)
                    {
                        foreach (Statement statement in tuple.Item2.Statements)
                        {
                            statement.Execute(interpret);
                        }
                        return;
                    }
                }

                if (ElseBranch != null)
                {
                    foreach (Statement statement in ElseBranch.Statements)
                    {
                        statement.Execute(interpret);
                    }
                }
            }
        }
    }
}
