namespace InterpreterLib.AST
{
    public class BinaryExpression : Expression
    {
        protected Expression left;
        protected Expression right;

        public BinaryExpression(Expression left, Expression right)
        {
            this.left = left;
            this.right = right;
        }
    }
}
