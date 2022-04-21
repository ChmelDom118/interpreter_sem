using InterpreterLib.AST;
using InterpreterLib.Lexer;

namespace InterpreterLib.Parser
{
    public class Parser
    {
        private List<Token> tokens;
        private int pointer;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            pointer = 0;
        }

        public Program Parse()
        {
            List<Function>? functions = null;

            if (Match(Tuple.Create(TokenType.Keyword, "declare")))
            {
                Consume(TokenType.Keyword, "declare");
                Consume(TokenType.Colon);
                functions = ReadFunctionDefinitions();
            }

            Consume(TokenType.Keyword, "begin");
            Consume(TokenType.Colon);

            Block block = ReadBlock();

            return new Program(
                block, 
                functions != null ? functions : new List<Function>()
            );
        }

        private Block ReadBlock()
        {
            Block block = new Block();

            while (Match(TokenType.Identifier, TokenType.Datatype)
                || Match(Tuple.Create(TokenType.Keyword, "for"))
                || Match(Tuple.Create(TokenType.Keyword, "while"))
                || Match(Tuple.Create(TokenType.Keyword, "repeat"))
                || Match(Tuple.Create(TokenType.Keyword, "if"))
                || Match(Tuple.Create(TokenType.Keyword, "execute"))
                )
            {
                block.AddStatements(ReadStatement());
            }

            return block;
        }

        private List<Function> ReadFunctionDefinitions()
        {
            List<Function> functions = new List<Function>();

            functions.Add(ReadFunctionDefinition());

            while (Match(Tuple.Create(TokenType.Keyword, "function")))
            {
                functions.Add(ReadFunctionDefinition());
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
                Expression expression = ReadExpression();
                bool isCondition = Match(
                    TokenType.EqualsTo,
                    TokenType.NotEqualsTo,
                    TokenType.LessThan,
                    TokenType.LessThanOrEqualTo,
                    TokenType.GreaterThan,
                    TokenType.GreaterThanOrEqualTo
                );

                switch (isCondition)
                {
                    case true:

                        returnArgument = new Argument(ReadCondition(expression));
                        Consume(TokenType.Semicolon);
                        break;
                    case false:

                        Consume(TokenType.Semicolon);
                        returnArgument = new Argument(expression);
                        break;
                }
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
                _ => throw new Exception("Parser: Not a valid datatype."),
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
                    default: throw new Exception("Parser: Invalid statement. Looking for one of these: {while, repeat, for, if, execute}.");
                }
            }
            else if (Match(TokenType.Datatype, TokenType.Identifier))
            {
                return ReadAssignStatement();
            }
            else
            {
                throw new Exception("Parser: Invalid statement. Looking for one of tehse: {while, repeat, for, if, execute}.");
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
            Consume(TokenType.Keyword, "for");
            Token identifierToken = CheckedReadToken(TokenType.Identifier);
            Consume(TokenType.Assign);
            Expression initExpression = ReadExpression();
            Consume(TokenType.Semicolon);
            Condition condition = ReadCondition();
            Consume(TokenType.Semicolon);


            Token? incrementIdentifierToken = null;
            Expression? incrementExpression = null;
            if (Match(TokenType.Identifier))
            {
                incrementIdentifierToken = CheckedReadToken(TokenType.Identifier);
                Consume(TokenType.Assign);
                incrementExpression = ReadExpression();
                Consume(TokenType.Semicolon);
            }

            Consume(TokenType.Keyword, "do");
            Block block = ReadBlock();
            Consume(TokenType.Keyword, "end");
            Consume(TokenType.Semicolon);
            return new ForStatement(
                identifierToken.Lexeme, 
                initExpression, 
                condition, 
                incrementIdentifierToken?.Lexeme, 
                incrementExpression, 
                block
            );
        }

        private IfStatement ReadIfStatement()
        {
            Consume(TokenType.Keyword, "if");
            Condition condition = ReadCondition();
            Consume(TokenType.Keyword, "then");
            Block block = ReadBlock();

            List<Tuple<Condition, Block>> elseIfBranches = new List<Tuple<Condition, Block>>();

            while (Match(Tuple.Create(TokenType.Keyword, "elseif")))
            {
                Consume(TokenType.Keyword, "elseif");
                Condition elseIfCondition = ReadCondition();
                Consume(TokenType.Keyword, "then");
                Block elseIfBlock = ReadBlock();
                elseIfBranches.Add(new Tuple<Condition, Block>(elseIfCondition, elseIfBlock));
            }

            Block? elseBranch = null;
            if (Match(Tuple.Create(TokenType.Keyword, "else")))
            {
                Consume(TokenType.Keyword, "else");
                elseBranch = ReadBlock();
            }

            Consume(TokenType.Keyword, "end");
            Consume(TokenType.Semicolon);

            return new IfStatement(condition, block, elseIfBranches, elseBranch);
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

        private FunctionCallExpression ReadFunctionCallExpression()
        {
            Consume(TokenType.Keyword, "execute");
            Token identifierToken = CheckedReadToken(TokenType.Identifier);
            Consume(TokenType.LeftPaprenthesis);
            List<Argument> arguments = ReadArguments();
            Consume(TokenType.RightParenthesis);
            return new FunctionCallExpression(identifierToken.Lexeme, arguments);
        }

        private AssignStatement ReadAssignStatement()
        {
            if (Match(TokenType.Datatype))
            {
                Token dataTypeToken = ReadToken();
                DataType? dataType = null;

                switch (dataTypeToken.Lexeme)
                {
                    case "integer": dataType = DataType.Integer; break;
                    case "double": dataType = DataType.Double; break;
                    case "string": dataType = DataType.String; break;
                    case "boolean": dataType = DataType.Boolean; break;
                }

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

                    switch (isCondition)
                    {
                        case true:

                            AssignStatement assigmentStatement = new AssignStatement(dataType, identifierToken.Lexeme, ReadCondition(expression));
                            Consume(TokenType.Semicolon);
                            return assigmentStatement;
                        case false:

                            Consume(TokenType.Semicolon);
                            return new AssignStatement(dataType, identifierToken.Lexeme, null, expression);
                    }
                }
                else
                {
                    Consume(TokenType.Semicolon);
                    return new AssignStatement(dataType, identifierToken.Lexeme);
                }
            }
            else
            {
                Token identifierToken = CheckedReadToken(TokenType.Identifier);
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

                switch (isCondition)
                {
                    case true:

                        AssignStatement assigmentStatement = new AssignStatement(null, identifierToken.Lexeme, ReadCondition(expression));
                        Consume(TokenType.Semicolon);
                        return assigmentStatement;
                    case false:

                        Consume(TokenType.Semicolon);
                        return new AssignStatement(null, identifierToken.Lexeme, null, expression);
                }
            }
        }

        private Condition ReadCondition(Expression? left = null)
        {
            if (
                Match(TokenType.Literal, TokenType.Identifier) 
                && !Match(
                    1, 
                    TokenType.EqualsTo,
                    TokenType.NotEqualsTo, 
                    TokenType.LessThan, 
                    TokenType.LessThanOrEqualTo, 
                    TokenType.GreaterThan, 
                    TokenType.GreaterThanOrEqualTo
                    )
                )
            {
                Token token = ReadToken();

                switch (token.Type)
                { 
                    case TokenType.Literal:

                        return new LiteralCondition(token.Lexeme);
                    case TokenType.Identifier:

                        return new IdentCondition(token.Lexeme);
                    default:
                        throw new Exception("Parser: Invalid single value condition. Expecting identifier or boolean literal.");
                }
            }
            else
            {
                if (left == null) left = ReadExpression();
                Token opToken = ReadToken();
                Expression right = ReadExpression();

                return opToken.Type switch
                {
                    TokenType.EqualsTo => new EqualsCondition(left, right),
                    TokenType.NotEqualsTo => new NotEqualsCondition(left, right),
                    TokenType.LessThan => new LessCondition(left, right),
                    TokenType.LessThanOrEqualTo => new LessEqualCondition(left, right),
                    TokenType.GreaterThan => new GreaterCondition(left, right),
                    TokenType.GreaterThanOrEqualTo => new GreaterEqualCondition(left, right),
                    _ => throw new Exception("Parser: invalid condition.")
                };
            }
        }

        private Expression ReadExpression()
        {
            Expression leftExpression;

            if (Match(TokenType.PlusSign))
            {
                Consume(TokenType.PlusSign);
                leftExpression = new PlusUnaryExpression(ReadTerm());
            }
            else if (Match(TokenType.PlusSign))
            {
                Consume(TokenType.MinusSign);
                leftExpression = new MinusUnaryExpression(ReadTerm());
            }
            else
            {
                leftExpression = ReadTerm();
            }

            while (Match(TokenType.PlusSign, TokenType.MinusSign))
            {
                Token operatorToken = ReadToken();
                Expression rightExpression = ReadTerm();

                switch (operatorToken.Type)
                {
                    case TokenType.PlusSign:
                        leftExpression = new Plus(leftExpression, rightExpression);
                        break;
                    case TokenType.MinusSign:
                        leftExpression = new Minus(leftExpression, rightExpression);
                        break;
                }
            }

            return leftExpression;
        }

        private Expression ReadTerm()
        {
            Expression leftExpression = ReadFactor();

            while (Match(TokenType.MultiplicationSign, TokenType.DivisionSign))
            {
                Token operatorToken = ReadToken();
                Expression rightExpression = ReadFactor();

                switch (operatorToken.Type)
                {
                    case TokenType.MultiplicationSign:
                        leftExpression = new Multiply(leftExpression, rightExpression);
                        break;
                    case TokenType.DivisionSign:
                        leftExpression = new Divide(leftExpression, rightExpression);
                        break;
                }
            }

            return leftExpression;
        }

        private Expression ReadFactor()
        {
            if (Match(TokenType.LeftPaprenthesis))
            {
                Consume(TokenType.LeftPaprenthesis);
                Expression expression = ReadExpression();
                Consume(TokenType.RightParenthesis);
                return expression;
            }
            else if (Match(Tuple.Create(TokenType.Keyword, "execute")))
            {
                return ReadFunctionCallExpression();
            }
            else if (Match(TokenType.Identifier))
            {
                Token identifierToken = ReadToken();
                return new IdentExpression(identifierToken.Lexeme);
            }
            else if (Match(TokenType.Literal))
            {
                Token literalToken = ReadToken();
                return new LiteralExpression(literalToken.Lexeme);
            }

            throw new Exception("Parser: invalid factor.");
        }

        private Token PeekToken()
        {
            return tokens[pointer];
        }

        private Token ReadToken()
        {
            return tokens[pointer++];
        }

        private Token CheckedReadToken(TokenType expectedType)
        {
            Token token = ReadToken();
            if (token.Type != expectedType) throw new Exception($"Parser: Expected \"{expectedType}\" token.");
            return token;
        }

        private Token CheckedReadToken(TokenType expectedType, string lexeme)
        {
            Token token = ReadToken();
            if (token.Type != expectedType || token.Lexeme != lexeme) throw new Exception($"Parser: Expected \"{expectedType}\" token with value of \"{lexeme}\" but value is \"{token.Lexeme}\".");
            return token;
        }

        private void Consume(TokenType tokenType)
        {
            Token token = ReadToken();
            if (token.Type != tokenType) throw new Exception($"Parser: Expected \"{tokenType}\" token.");
        }

        private void Consume(TokenType tokenType, string lexeme)
        {
            Token token = ReadToken();
            if (token.Type != tokenType || (token.Type == tokenType && token.Lexeme != lexeme)) throw new Exception($"Parser: Expected \"{tokenType}\" token with value of \"{lexeme}\" but value is \"{token.Lexeme}\".");
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

        private bool Match(int offset, params TokenType[] tokenTypes)
        {
            Token token = tokens[pointer + offset];

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
