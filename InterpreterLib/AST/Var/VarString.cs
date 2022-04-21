namespace InterpreterLib.AST
{
    public class VarString : Var
    {
        public string Value { get; set; }

        public VarString(string identifier, string value) : base(identifier, DataType.String)
        {
            Value = value;
        }
    }
}
