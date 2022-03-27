namespace InterpreterLib.AST
{
    public class Program
    {
        public List<Function> Functions { get; private set; }
        public Block Block { get; private set; }

        public Program(Block block, List<Function> functions)
        {
            Block = block;
            Functions = functions;
        }
    }
}
