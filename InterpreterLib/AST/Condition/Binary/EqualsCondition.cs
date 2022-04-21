using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public class EqualsCondition : BinaryRelCondition
    {
        public EqualsCondition(Expression left, Expression right) : base(left, right) { }

        public override Var Evaluate(Interpreter interpret)
        {
            Var left = LeftExpression.Evaluate(interpret);
            Var right = RightExpression.Evaluate(interpret);

            if (left is VarInt leftInt && right is VarInt rightInt)
            {
                return new VarBool(null, leftInt.Value == rightInt.Value);
            }
            else if (left is VarDouble leftDouble && right is VarDouble rightDouble)
            {
                return new VarBool(null, leftDouble.Value == rightDouble.Value);
            }
            else if (left is VarString leftString && right is VarString rightString)
            {
                return new VarBool(null, string.Compare(leftString.Value, rightString.Value) == 0);
            }
            else if (left is VarBool leftBool && right is VarBool rightBool)
            {
                return new VarBool(null, leftBool.Value == rightBool.Value);
            }

            throw new Exception("Unsupported operation!");
        }
    }
}
