namespace InterpreterLib.AST
{
    public class VarInt : Var
    {
        public int? Value { get; set; }

        public VarInt(string identifier, int? value) : base(identifier, DataType.Integer)
        {
            Value = value;
        }
    }
}
