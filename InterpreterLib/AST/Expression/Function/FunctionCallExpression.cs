using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public class FunctionCallExpression : Expression
    {
        public string Identifier { get; }
        public List<Argument> Arguments { get; }

        public FunctionCallExpression(string identifier, List<Argument> arguments)
        {
            Identifier = identifier;
            Arguments = arguments;
        }

        public override Var Evaluate(Interpreter interpret)
        {
            Var var = interpret.CallFunction(Identifier, Arguments);
            if (var == null) throw new Exception("FunctionCallExpression: return value must not be null.");
            return var;
        }
    }
}
