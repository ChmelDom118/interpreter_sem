using InterpreterLib.AST;

namespace InterpreterLib.Interpret
{
    public class Variables
    {
        private readonly Dictionary<string, Var> _variables;

        public Variables(List<Var> vars)
        {
            _variables = new Dictionary<string, Var>();
            vars?.ForEach(var => _variables.Add(var.Identifier, var));
        }

        public bool Add(Var newVariable)
        {
            if (_variables.ContainsKey(newVariable.Identifier)) return false;
            _variables.Add(newVariable.Identifier, newVariable);
            return true;
        }

        public Var? Get(string identifier)
        {
            bool success = _variables.TryGetValue(identifier, out Var var);
            if (success) return var;
            return null;
        }

        public bool Set(Var newValue)
        {
            if (Remove(newValue))
            {
                Add(newValue);
                return true;
            }

            return false;
        }

        public bool Remove(Var var)
        {
            bool success = _variables.TryGetValue(var.Identifier, out Var v);

            if (success && v.DataType == var.DataType)
            {
                return _variables.Remove(var.Identifier);
            }

            return false;
        }
    }
}
