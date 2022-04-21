using InterpreterLib.Interpret;

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

        public override void Execute(Interpreter interpret)
        {
            if (Condition.Evaluate(interpret) is VarBool conditionResult)
            {
                foreach (Statement statement in Block.Statements)
                {
                    statement.Execute(interpret);
                }

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
