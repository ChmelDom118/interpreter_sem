namespace InterpreterLib.AST
{
    public class BinaryExpression
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
