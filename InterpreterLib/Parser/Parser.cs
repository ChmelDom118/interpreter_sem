using InterpreterLib.Lexer;
using InterpreterLib.AST;

namespace InterpreterLib.Parser
{
    public class Parser
    {
        private List<Token> _tokens;
        private int _pointer;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
            _pointer = 0;
        }

        public Program Parse()
        {
            List<Function>? functions = null;

            if (Match(Tuple.Create(TokenType.Keyword, "declare")))
            {
                Consume(TokenType.Colon);
                functions = ReadFunctionDefinitions();
            }

            Block block = ReadBlock();

            return new Program(
                block, 
                functions != null ? functions : new List<Function>()
            );
        }

        private Block ReadBlock()
        {
            Block block = new Block();

            Token token = PeekToken();

            while (Match(TokenType.Keyword, TokenType.Identifier, TokenType.Datatype))
            {
                block.AddStatements(ReadStatement());
                token = PeekToken();
            }

            return block;
        }

        private List<Function> ReadFunctionDefinitions()
        {
            List<Function> functions = new List<Function>();

            functions.Add(ReadFunctionDefinition());

            Token token = PeekToken();

            while (token.Type == TokenType.Keyword && token.Lexeme == "function")
            {
                functions.Add(ReadFunctionDefinition());
                token = PeekToken();
            }

            return functions;
        }

        private Function ReadFunctionDefinition()
        {
            Consume(TokenType.Keyword, "function");
            Token identifierToken = CheckedReadToken(TokenType.Identifier);
            Consume(TokenType.LeftPaprenthesis);
            List<Parameter> parameters = ReadParameters();
            Consume(TokenType.RightParenthesis);
            Token token = PeekToken();

            DataType? dataType = null;
            if (token.Type == TokenType.Arrow)
            {
                Consume(TokenType.Arrow);
                Token dataTypeToken = CheckedReadToken(TokenType.Datatype);
                switch (dataTypeToken.Lexeme)
                {
                    case "integer": dataType = DataType.Integer; break;
                    case "double": dataType = DataType.Double; break;
                    case "string": dataType = DataType.String; break;
                    case "boolean": dataType = DataType.Boolean; break;
                }
            }

            Block block = ReadBlock();

            Argument? returnArgument = null;

            if (dataType.HasValue)
            {
                Consume(TokenType.Keyword, "return");
                ReadCondition() / ReadExpression(); // vytvorit argument (condition / statement)
            }

            Consume(TokenType.Keyword, "end");
            Consume(TokenType.Semicolon);

            return new Function(identifierToken.Lexeme, parameters, block, dataType, returnArgument);
        }

        private List<Parameter> ReadParameters()
        {
            List<Parameter> parameters = new List<Parameter>();

            while (Match(TokenType.Datatype))
            {
                parameters.Add(ReadParameter());
                if (!Match(TokenType.Comma)) break;
                Consume(TokenType.Comma);
            }

            return parameters;
        }

        private Parameter ReadParameter()
        {
            Token dataTypeToken = ReadToken();
            Token identifierToken = CheckedReadToken(TokenType.Identifier);

            return dataTypeToken.Lexeme switch
            {
                "integer" => new Parameter(DataType.Integer, identifierToken.Lexeme),
                "double" => new Parameter(DataType.Double, identifierToken.Lexeme),
                "string" => new Parameter(DataType.String, identifierToken.Lexeme),
                "boolean" => new Parameter(DataType.Boolean, identifierToken.Lexeme),
                _ => throw new Exception("Not a valid datatype."),
            };
        }

        private List<Argument> ReadArguments()
        {
            List<Argument> arguments = new List<Argument>();

            while (!Match(TokenType.RightParenthesis))
            {
                arguments.Add(ReadArgument());
                if (!Match(TokenType.Comma)) break;
                Consume(TokenType.Comma);
            }

            return arguments;
        }

        private Argument ReadArgument()
        {
            Expression expression = ReadExpression();
            bool isCondition = Match(
                TokenType.EqualsTo,
                TokenType.NotEqualsTo,
                TokenType.LessThan,
                TokenType.LessThanOrEqualTo,
                TokenType.GreaterThan,
                TokenType.GreaterThanOrEqualTo
            );

            return isCondition 
                ? new Argument(ReadCondition(expression)) 
                : new Argument(expression);
        }

        private Statement ReadStatement()
        {
            if (Match(TokenType.Keyword))
            {
                switch (PeekToken().Lexeme)
                {
                    case "while": return ReadWhileStatement();
                    case "repeat": return ReadRepeatUntilStatement();
                    case "for": return ReadForStatement();
                    case "if": return ReadIfStatement();
                    case "execute": return ReadFunctionCallStatement();
                    default: throw new Exception("Invalid statement.");
                }
            }
            else if (Match(TokenType.Datatype, TokenType.Identifier))
            {
                return ReadAssignStatement();
            }
            else
            {
                throw new Exception("Invalid statement.");
            }
        }

        private WhileStatement ReadWhileStatement()
        {
            Consume(TokenType.Keyword, "while");
            Condition condition = ReadCondition();
            Consume(TokenType.Keyword, "do");
            Block block = ReadBlock();
            Consume(TokenType.Keyword, "end");
            Consume(TokenType.Semicolon);
            return new WhileStatement(condition, block);
        }

        private RepeatUntilStatement ReadRepeatUntilStatement()
        {
            Consume(TokenType.Keyword, "repeat");
            Block block = ReadBlock();
            Consume(TokenType.Keyword, "until");
            Condition condition = ReadCondition();
            Consume(TokenType.Keyword, "end");
            Consume(TokenType.Semicolon);
            return new RepeatUntilStatement(condition, block);
        }

        private ForStatement ReadForStatement()
        {

        }

        private IfStatement ReadIfStatement()
        {

        }

        private FunctionCallStatement ReadFunctionCallStatement()
        {
            Consume(TokenType.Keyword, "execute");
            Token identifierToken = CheckedReadToken(TokenType.Identifier);
            Consume(TokenType.LeftPaprenthesis);
            List<Argument> arguments = ReadArguments();
            Consume(TokenType.RightParenthesis);
            Consume(TokenType.Semicolon);
            return new FunctionCallStatement(identifierToken.Lexeme, arguments);
        }

        private AssignStatement ReadAssignStatement()
        {
            if (Match(TokenType.Datatype))
            {
                Token dataTypeToken = ReadToken();
                Token identifierToken = CheckedReadToken(TokenType.Identifier);
                if (Match(TokenType.Assign))
                {
                    Consume(TokenType.Assign);
                    Expression expression = ReadExpression();
                    bool isCondition = Match(
                        TokenType.EqualsTo,
                        TokenType.NotEqualsTo,
                        TokenType.LessThan,
                        TokenType.LessThanOrEqualTo,
                        TokenType.GreaterThan,
                        TokenType.GreaterThanOrEqualTo
                    );
                    return new AssignStatement();
                }
            }
            else
            {

            }
        }

        private Condition ReadCondition(Expression? expression = null)
        {

        }

        private Expression ReadExpression()
        {

        }

        private Token PeekToken()
        {
            return _tokens[_pointer];
        }

        private Token ReadToken()
        {
            return _tokens[_pointer++];
        }

        private Token CheckedReadToken(TokenType expectedType)
        {
            Token token = ReadToken();
            if (token.Type != expectedType) throw new Exception($"Expected \"{expectedType}\" token.");
            return token;
        }

        private Token CheckedReadToken(TokenType expectedType, string lexeme)
        {
            Token token = ReadToken();
            if (token.Type != expectedType || token.Lexeme != lexeme) throw new Exception($"Expected \"{expectedType}\" token.");
            return token;
        }

        private void Consume(TokenType tokenType)
        {
            Token token = ReadToken();
            if (token.Type != tokenType) throw new Exception($"Expected \"{tokenType}\" token.");
        }

        private void Consume(TokenType tokenType, string lexeme)
        {
            Token token = ReadToken();
            if (token.Type != tokenType || token.Lexeme != lexeme) throw new Exception($"Expected \"{tokenType}\" token.");
        }

        private bool Match(params TokenType[] tokenTypes)
        {
            Token token = PeekToken();

            foreach (TokenType tokenType in tokenTypes)
            {
                if (tokenType == token.Type) return true;
            }

            return false;
        }

        private bool Match(params Tuple<TokenType, string>[] tokens)
        {
            Token token = PeekToken();

            foreach (Tuple<TokenType, string> expectedToken in tokens)
            {
                if (expectedToken.Item1 == token.Type && expectedToken.Item2 == token.Lexeme) 
                    return true;
            }

            return false;
        }
    }
}
