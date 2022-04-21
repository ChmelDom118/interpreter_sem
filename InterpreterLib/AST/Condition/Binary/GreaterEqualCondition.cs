﻿using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public class GreaterEqualCondition : BinaryRelCondition
    {
        public GreaterEqualCondition(Expression left, Expression right) : base(left, right) { }

        public override Var Evaluate(Interpreter interpret)
        {
            Var left = LeftExpression.Evaluate(interpret);
            Var right = RightExpression.Evaluate(interpret);

            if (left is VarInt leftInt && right is VarInt rightInt)
            {
                return new VarBool(null, leftInt.Value >= rightInt.Value);
            }
            else if (left is VarDouble leftDouble && right is VarDouble rightDouble)
            {
                return new VarBool(null, leftDouble.Value >= rightDouble.Value);
            }
            throw new Exception("Unsupported operation!");
        }
    }
}