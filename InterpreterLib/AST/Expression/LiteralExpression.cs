using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public class LiteralExpression : Expression
    {
        protected string Value;

        public LiteralExpression(string value)
        {
            Value = value;
        }

        public override Var Evaluate(Interpreter interpret)
        {
            switch (Value)
            {
                case "true":

                    return new VarBool(null, true);
                case "false":

                    return new VarBool(null, false);
                default:

                    if (Value.Contains('.'))
                    {
                        bool success = double.TryParse(Value.Replace('.', ','), out double result);

                        if (success)
                        {
                            return new VarDouble(null, result);
                        }
                        else
                        {
                            return new VarString(null, Value);
                        }
                    }
                    else
                    {
                        bool success = int.TryParse(Value, out int result);

                        if (success)
                        {
                            return new VarInt(null, result);
                        }
                        else
                        {
                            return new VarString(null, Value);
                        }
                    }
            }
        }
    }
}
