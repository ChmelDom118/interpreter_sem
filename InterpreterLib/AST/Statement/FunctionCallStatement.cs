﻿using InterpreterLib.Interpret;

namespace InterpreterLib.AST
{
    public class FunctionCallStatement : Statement
    {
        public string Identifier { get; private set; }
        public List<Argument> Arguments { get; private set; }

        public FunctionCallStatement(string identifier, List<Argument> arguments)
        {
            Identifier = identifier;
            Arguments = arguments;
        }

        public override void Execute(Interpreter interpter)
        {
            interpter.CallFunction(Identifier, Arguments);
        }
    }
}
