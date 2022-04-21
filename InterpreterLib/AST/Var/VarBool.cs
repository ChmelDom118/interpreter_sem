namespace InterpreterLib.AST
{
    public class VarBool : Var
    {
        public bool? Value { get; set; }

        public VarBool(string identifier, bool? value) : base(identifier, DataType.Boolean)
        {
            Value = value;
        }
    }
}
