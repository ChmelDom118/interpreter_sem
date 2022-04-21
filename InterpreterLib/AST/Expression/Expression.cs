using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public abstract class Expression 
    {
        public abstract Var Evaluate(Interpreter interpret);
    }
}
