using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public class PrintStatement : Statement
    {
        string Identifier { get; }

        public PrintStatement(string identifier)
        {
            Identifier = identifier;
        }

        public override void Execute(Interpreter interpret)
        {
            Var? var = interpret.CurrentContext.Variables.Get(Identifier);
            if (var == null) throw new Exception("Cannot print unknown variable.");

            switch (var.DataType)
            {
                case DataType.Integer:

                    VarInt varInt = (VarInt)var;
                    Console.WriteLine(varInt.Value);
                    interpret.output.Add($"{varInt.Value}");
                    break;
                case DataType.Double:

                    VarDouble varDouble = (VarDouble)var;
                    string output = $"{varDouble.Value}".Replace(',', '.');
                    Console.WriteLine(output);
                    interpret.output.Add($"{output}");
                    break;
                case DataType.String:

                    VarString varString = (VarString)var;
                    Console.WriteLine(varString.Value);
                    interpret.output.Add($"{varString.Value}");
                    break;
                case DataType.Boolean:

                    VarBool varBool = (VarBool)var;
                    Console.WriteLine(varBool.Value);
                    interpret.output.Add($"{varBool.Value}");
                    break;
            }
        }
    }
}
