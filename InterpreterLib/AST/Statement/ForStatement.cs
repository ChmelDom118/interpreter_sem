using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public class ForStatement : Statement
    {
        public string Identifier { get; private set; }
        public Expression InitExpression { get; private set; }
        public Condition Condition { get; private set; }
        public string? IncrementIdentifier { get; private set; }
        public Expression? IncrementExpression { get; set; }
        public Block Block { get; private set; }

        public ForStatement(string identifier, Expression initExpression, Condition condition, string? incrementIdentifier, Expression? incrementExpression, Block block)
        {
            Identifier = identifier;
            InitExpression = initExpression;
            Condition = condition;
            IncrementIdentifier = incrementIdentifier;
            IncrementExpression = incrementExpression;
            Block = block;
        }

        public override void Execute(Interpreter interpret)
        {
            if (InitExpression.Evaluate(interpret) is VarInt initExpressionResult)
            {
                Var var = new VarInt(Identifier, initExpressionResult.Value);
                interpret.CurrentContext.Variables.Add(var);
                
                while (Condition.Evaluate(interpret) is VarBool conditionResult && conditionResult.Value == true)
                {
                    foreach (Statement statement in Block.Statements)
                    {
                        statement.Execute(interpret);
                    }

                    if (IncrementExpression != null && IncrementIdentifier != null)
                    {
                        VarInt? result = IncrementExpression.Evaluate(interpret) as VarInt;
                        if (result != null)
                        {
                            bool success = interpret.CurrentContext.Variables.Set(new VarInt(IncrementIdentifier, result.Value));
                            if (!success) throw new Exception("ForStatement: unknown variable in incrementation part.");
                        }
                    }
                }

                interpret.CurrentContext.Variables.Remove(var);
            }
        }
    }
}
