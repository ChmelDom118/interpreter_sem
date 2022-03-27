namespace InterpreterLib.AST
{
    public class Block
    {
        public List<Statement> Statements { get; private set; }

        public Block()
        {
            Statements = new List<Statement>();
        }

        public void AddStatements(params Statement[] statements)
        {
            Statements.AddRange(statements);
        }
    }
}
