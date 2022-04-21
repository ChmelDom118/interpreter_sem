using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public class AssignStatement : Statement
    {
        public DataType? DataType { get; }
        public string Identifier { get; }
        public Condition? Condition { get; }
        public Expression? Expression { get; }

        public AssignStatement(DataType? dataType, string idenifier, Condition? condition = null, Expression? expression = null)
        {
            DataType = dataType;
            Identifier = idenifier;
            Condition = condition;
            Expression = expression;
        }

        public override void Execute(Interpreter interpret)
        {
            Var? var = interpret.CurrentContext.Variables.Get(Identifier);
            Var? result = Condition?.Evaluate(interpret) ?? Expression?.Evaluate(interpret);

            if (DataType.HasValue)
            {
                if (result != null)
                {
                    if (DataType != result.DataType) throw new Exception($"AssignStatement: cannot assign value of {result.DataType} to {DataType}.");

                    bool success;
                    Var newVar;

                    switch (DataType.Value)
                    {
                        case AST.DataType.Integer:

                            VarInt varInt = (VarInt)result;
                            newVar = new VarInt(Identifier, varInt.Value);
                            success = interpret.CurrentContext.Variables.Add(newVar);
                            if (success == false) interpret.CurrentContext.Variables.Set(newVar);
                            break;
                        case AST.DataType.Double:

                            VarDouble varDouble = (VarDouble)result;
                            newVar = new VarDouble(Identifier, varDouble.Value);
                            success = interpret.CurrentContext.Variables.Add(newVar);
                            if (success == false) interpret.CurrentContext.Variables.Set(newVar);
                            break;
                        case AST.DataType.String:

                            VarString varString = (VarString)result;
                            newVar = new VarString(Identifier, varString.Value);
                            success = interpret.CurrentContext.Variables.Add(newVar);
                            if (success == false) interpret.CurrentContext.Variables.Set(newVar);
                            break;
                        case AST.DataType.Boolean:

                            VarBool varBool = (VarBool)result;
                            newVar = new VarBool(Identifier, varBool.Value);
                            success = interpret.CurrentContext.Variables.Add(new VarBool(Identifier, varBool.Value));
                            if (success == false) interpret.CurrentContext.Variables.Set(newVar);
                            break;
                    }
                }
                else
                {
                    switch (DataType.Value)
                    {
                        case AST.DataType.Integer:

                            interpret.CurrentContext.Variables.Add(new VarInt(Identifier, 0));
                            break;
                        case AST.DataType.Double:

                            interpret.CurrentContext.Variables.Add(new VarDouble(Identifier, 0.0));
                            break;
                        case AST.DataType.String:

                            interpret.CurrentContext.Variables.Add(new VarString(Identifier, ""));
                            break;
                        case AST.DataType.Boolean:

                            interpret.CurrentContext.Variables.Add(new VarBool(Identifier, false));
                            break;
                    }
                }
            }
            else
            {
                if (result != null)
                {
                    bool success = false;

                    switch (result.DataType)
                    {
                        case AST.DataType.Integer:

                            VarInt varInt = (VarInt)result;
                            success = interpret.CurrentContext.Variables.Set(new VarInt(Identifier, varInt.Value));
                            break;
                        case AST.DataType.Double:

                            VarDouble varDouble = (VarDouble)result;
                            success = interpret.CurrentContext.Variables.Set(new VarDouble(Identifier, varDouble.Value));
                            break;
                        case AST.DataType.String:

                            VarString varString = (VarString)result;
                            success = interpret.CurrentContext.Variables.Set(new VarString(Identifier, varString.Value));
                            break;
                        case AST.DataType.Boolean:

                            VarBool varBool = (VarBool)result;
                            success = interpret.CurrentContext.Variables.Set(new VarBool(Identifier, varBool.Value));
                            break;
                    }

                    if (!success) throw new Exception("AssignStatement: cannot assign value to undeclared variable.");
                }
                else
                {
                    throw new Exception("AssignStatement: cannot assign null value.");
                }
            }
        }
    }
}
