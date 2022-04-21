using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public class MinusUnaryExpression : UnaryExpression
    {
        public MinusUnaryExpression(Expression expression) : base(expression) { }

        public override Var Evaluate(Interpreter interpret)
        {
            Var result = Expression.Evaluate(interpret);

            if (result is VarInt varInt)
            {
                return new VarInt(null, -varInt.Value);
            }
            else if (result is VarDouble varDouble)
            {
                return new VarDouble(null, -varDouble.Value);
            }

            throw new Exception("Unsupported operation!");
        }
    }
}
