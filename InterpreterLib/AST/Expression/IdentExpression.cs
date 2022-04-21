using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public class IdentExpression : Expression
    {
        public string Identifier { get; private set; }

        public IdentExpression(string identifier)
        {
            Identifier = identifier;
        }

        public override Var Evaluate(Interpreter interpret)
        {
            Var? var = interpret.CurrentContext.Variables.Get(Identifier);
            if (var == null) throw new Exception("IdentExpression: variable does not exists.");
            return var;
        }
    }
}
