using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public class Plus : BinaryExpression
    {
        public Plus(Expression left, Expression right) : base(left, right) { }

        public override Var Evaluate(Interpreter interpret)
        {
            Var left = LeftExpression.Evaluate(interpret);
            Var right = RightExpression.Evaluate(interpret);

            if (left is VarInt leftInt && right is VarInt rightInt)
            {
                return new VarInt(null, leftInt.Value + rightInt.Value);
            }
            else if (left is VarDouble leftDouble && right is VarDouble rightDouble)
            {
                return new VarDouble(null, leftDouble.Value + rightDouble.Value);
            }
            else if (left is VarString leftString && right is VarString rightString)
            {
                return new VarString(null, leftString.Value + rightString.Value);
            }
            else if (left is VarString leftString2 && right is VarInt rightInt2)
            {
                return new VarString(null, $"{leftString2.Value}{rightInt2.Value}");
            }
            throw new Exception("Unsupported operation!");
        }
    }
}
