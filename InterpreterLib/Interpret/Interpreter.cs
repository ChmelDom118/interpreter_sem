using InterpreterLib.AST;

namespace InterpreterLib.Interpret
{
    public class Interpreter
    {
        private readonly Dictionary<string, Function> functions;
        private Stack<ExecutionContext> stack;
        internal List<string> output;

        public Program Program { get; }

        public Interpreter(Program program)
        {
            Program = program;
            stack = new Stack<ExecutionContext>();
            output = new List<string>();

            functions = new Dictionary<string, Function>();
            program.Functions?.ForEach(function => {
                if (!functions.ContainsKey(function.Identifier))
                {
                    functions.Add(function.Identifier, function);
                }
                else
                {
                    throw new Exception($"Interpreter: Function with {function.Identifier} identifier has alredy been defined.");
                }
            });

            RegisterLibraryFunctions();
        }

        private void RegisterLibraryFunctions()
        {
            functions.Add("printInteger", new PrintFunction("printInteger", new Parameter(DataType.Integer, "value")));
            functions.Add("printDouble", new PrintFunction("printDouble", new Parameter(DataType.Double, "value")));
            functions.Add("printString", new PrintFunction("printString", new Parameter(DataType.String, "value")));
            functions.Add("printBoolean", new PrintFunction("printBoolean", new Parameter(DataType.Boolean, "value")));
        }

        public List<string> Interpret()
        {
            stack.Push(new ExecutionContext(new Variables(new List<Var>())));

            foreach (Statement statement in Program.Block.Statements)
            {
                statement.Execute(this);
            }

            return output;
        }

        internal ExecutionContext CurrentContext => stack.Peek();

        internal Var CallFunction(string functionIdentifier, List<Argument> arguments)
        {
            functions.TryGetValue(functionIdentifier, out Function? function);

            if (function != null)
            {
                if (function.Parameters.Count != arguments.Count) throw new Exception($"Interpreter: {functionIdentifier} -> Function call does not match definition (invalid count of arguments).");

                List<Var> processedArguments = new List<Var>();

                for (int i = 0; i < function.Parameters.Count; i++)
                {
                    Argument argument = arguments[i];
                    Parameter parameter = function.Parameters[i];

                    if (argument.Condition != null)
                    {
                        Var result = argument.Condition.Evaluate(this);
                        if (result.DataType != parameter.DataType) throw new Exception($"Interpreter: {functionIdentifier} -> The argument datatype does not match parameter datatype.");

                        if (result is VarBool varBool)
                        {
                            processedArguments.Add(new VarBool(parameter.Identifier, varBool.Value));
                        }
                        else
                        {
                            throw new Exception($"Interpreter: {functionIdentifier} -> The argument is type of condition. Result should be boolean value.");
                        }
                    }
                    else if (argument.Expression != null)
                    {
                        Var result = argument.Expression.Evaluate(this);
                        if (result.DataType != parameter.DataType) throw new Exception($"Interpreter: {functionIdentifier} -> The argument datatype does not match parameter datatype.");

                        switch (result.DataType)
                        {
                            case DataType.Integer:

                                VarInt varInt = (VarInt)result;
                                processedArguments.Add(new VarInt(parameter.Identifier, varInt.Value));
                                break;
                            case DataType.Double:

                                VarDouble varDouble = (VarDouble)result;
                                processedArguments.Add(new VarDouble(parameter.Identifier, varDouble.Value));
                                break;
                            case DataType.String:


                                VarString varString = (VarString)result;
                                processedArguments.Add(new VarString(parameter.Identifier, varString.Value));
                                break;
                            case DataType.Boolean:


                                VarBool varBool = (VarBool)result;
                                processedArguments.Add(new VarBool(parameter.Identifier, varBool.Value));
                                break;
                        }
                    }
                    else
                    {
                        throw new Exception($"Interpreter: {functionIdentifier} -> The argument does not contain condition nor expression.");
                    }
                }

                // switch context
                stack.Push(new ExecutionContext(new Variables(processedArguments)));

                foreach (Statement statement in function.Block.Statements)
                {
                    statement.Execute(this);
                }

                if (function.ReturnArgument?.Condition != null)
                {
                    Var returnVar = function.ReturnArgument.Condition.Evaluate(this);
                    stack.Pop();
                    if (returnVar.DataType != function.DataType) throw new Exception($"Interpreter: {functionIdentifier} -> Invalid datatype of return value. Cannot return value of {returnVar.DataType} when function return type is {function.DataType}.");
                    return returnVar;
                }
                else if (function.ReturnArgument?.Expression != null)
                {
                    Var returnVar = function.ReturnArgument.Expression.Evaluate(this);
                    stack.Pop();
                    if (returnVar.DataType != function.DataType) throw new Exception($"Interpreter: {functionIdentifier} -> Invalid datatype of return value. Cannot return value of {returnVar.DataType} when function return type is {function.DataType}.");
                    return returnVar;
                }
                else
                {
                    stack.Pop();
                    return null;
                }

                throw new Exception($"Interpreter: {functionIdentifier} -> Invalid function.");
            }
            else
            {
                throw new Exception($"Interpreter: {functionIdentifier} -> Cannot find function with \"{functionIdentifier}\" identifier.");
            }
        }
    }
}