namespace InterpreterLib.AST
{
    public class VarDouble : Var
    {
        public double? Value { get; set; }

        public VarDouble(string identifier, double? value) : base(identifier, DataType.Double)
        {
            Value = value;
        }
    }
}
