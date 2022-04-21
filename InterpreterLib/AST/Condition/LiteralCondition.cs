using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public class LiteralCondition : Condition
    {
        public string Value { get; private set; }

        public LiteralCondition(string value)
        {
            Value = value;
        }

        public override Var Evaluate(Interpreter interpret)
        {
            return Value switch
            {
                "true" => new VarBool(null, true),
                "false" => new VarBool(null, false),
                _ => throw new Exception("LiteralCondition: value should be true or false.")
            };
        }
    }
}
