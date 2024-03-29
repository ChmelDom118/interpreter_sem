﻿using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public class IdentCondition : Condition
    {
        public string Identifier { get; private set; }
        
        public IdentCondition(string identifier)
        {
            Identifier = identifier;
        }

        public override Var Evaluate(Interpreter interpret)
        {
            Var? var = interpret.CurrentContext.Variables.Get(Identifier);
            if (var == null) throw new Exception("IdentCondition: variable does not exists.");
            if (var is not VarBool) throw new Exception("IdentCondition: variable must be type of bool.");
            return var;
        }
    }
}
