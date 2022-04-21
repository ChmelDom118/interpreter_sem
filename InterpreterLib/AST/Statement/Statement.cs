using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public abstract class Statement
    {
        public abstract void Execute(Interpreter interpret);
    }
}
