using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public abstract class Condition 
    {
        public abstract Var Evaluate(Interpreter interpret);
    }
}
