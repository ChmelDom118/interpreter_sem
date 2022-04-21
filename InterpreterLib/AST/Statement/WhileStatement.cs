using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public class WhileStatement : Statement
    {
        public Condition Condition { get; }
        public Block Block { get; }

        public WhileStatement(Condition condition, Block block)
        {
            Condition = condition;
            Block = block;
        }

        public override void Execute(Interpreter interpret)
        {
            if (Condition.Evaluate(interpret) is VarBool conditionResult)
            {
                while (conditionResult.Value == true)
                {
                    foreach (Statement statement in Block.Statements)
                    {
                        statement.Execute(interpret);
                    }
                }
            }
        }
    }
}
