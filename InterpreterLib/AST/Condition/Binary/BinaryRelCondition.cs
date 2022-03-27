namespace InterpreterLib.AST
{
    public abstract class BinaryRelCondition : Condition
    {
        protected Expression left;
        protected Expression right;

        public BinaryRelCondition(Expression left, Expression right)
        {
            this.left = left;
            this.right = right;
        }
    }
}
