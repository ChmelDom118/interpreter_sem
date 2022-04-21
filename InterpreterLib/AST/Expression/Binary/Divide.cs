using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public class Divide : BinaryExpression
    {
        public Divide(Expression left, Expression right) : base(left, right) { }

        public override Var Evaluate(Interpreter interpret)
        {
            Var left = LeftExpression.Evaluate(interpret);
            Var right = RightExpression.Evaluate(interpret);

            if (left is VarInt leftInt && right is VarInt rightInt)
            {
                if (rightInt.Value == 0) throw new ArithmeticException("Cannot be divided by zero.");

                return new VarInt(null, leftInt.Value / rightInt.Value);
            }
            else if (left is VarDouble leftDouble && right is VarDouble rightDouble)
            {
                if (rightDouble.Value == 0) throw new ArithmeticException("Cannot be divided by zero.");

                return new VarDouble(null, leftDouble.Value / rightDouble.Value);
            }
            throw new Exception("Unsupported operation!");
        }
    }
}
