namespace InterpreterLib.Lexer
{
    public class Lexer
    {
        private readonly string _input;

        private int _start = 0;
        private int _current = 0;
        private int _line = 1;

        private List<Token> _tokens;

        public Lexer(string input)
        {
            _input = input;
            _tokens = new List<Token>();
        }

        public List<Token> Tokenize()
        {
            while (!IsAtTheEnd)
            {
                _start = _current;
                ReadToken();
            }

            _tokens.Add(new Token(TokenType.EOF, "", _line));
            return _tokens;
        }

        private bool IsAtTheEnd => _current >= _input.Length;

        private void ReadToken()
        {
            char character = _input[_current++];

            switch (character)
            {
                case '+': AddToken(TokenType.PlusSign); break;
                case '-': AddToken(IsNext('>') ? TokenType.Arrow: TokenType.MinusSign); break;
                case '*': AddToken(TokenType.MultiplicationSign); break;
                case '/': AddToken(TokenType.DivisionSign); break;
                case '=': AddToken(IsNext('=') ? TokenType.EqualsTo : TokenType.Assign); break;
                case '!': AddToken(IsNext('=') ? TokenType.NotEqualsTo : TokenType.Unknown); break;
                case '<': AddToken(IsNext('=') ? TokenType.LessThanOrEqualTo : TokenType.LessThan); break;
                case '>': AddToken(IsNext('=') ? TokenType.GreaterThanOrEqualTo : TokenType.GreaterThan); break;
                case '.': AddToken(TokenType.DecimalSeparator); break;
                case ',': AddToken(TokenType.Comma); break;
                case ';': AddToken(TokenType.Semicolon); break;
                case ':': AddToken(TokenType.Colon); break;
                case '(': AddToken(TokenType.LeftPaprenthesis); break;
                case ')': AddToken(TokenType.RightParenthesis); break;
                case '\"':
                    break;
                case ' ':
                case '\r':
                case '\t': break;
                case '\n': _line++; break;
                default:

                    if (char.IsLetter(character))
                    {
                        while (char.IsLetter(_input[_current]))
                        {
                            _current++;
                        }

                        switch (GetCurrentLexeme())
                        {
                            case "declare":
                            case "begin":
                            case "end":
                            case "while":
                            case "do":
                            case "repeat":
                            case "until":
                            case "for":
                            case "if":
                            case "then":
                            case "elseif":
                            case "else":
                            case "function":
                            case "return":
                            case "execute": AddToken(TokenType.Keyword); break;
                            case "integer":
                            case "double":
                            case "string":
                            case "bool": AddToken(TokenType.Datatype); break;
                            default: AddToken(TokenType.Identifier); break;
                        }
                    }
                    else if (char.IsDigit(character))
                    {
                        bool dotWas = false;

                        while (_current < _input.Length && (char.IsDigit(_input[_current])
                                                    || (_input[_current] == '.' && !dotWas)))
                        {
                            if (!dotWas)
                            {
                                dotWas = _input[_current] == '.';
                            }
                            _current++;
                        }

                        AddToken(TokenType.Literal);
                    }
                    else
                    {
                        AddToken(TokenType.Unknown);
                    }
                    break;
            }
        }

        private string GetCurrentLexeme()
        {
            return _input[_start.._current];
        }

        private void AddToken(TokenType type)
        {
            _tokens.Add(new Token(type, GetCurrentLexeme(), _line));
        }

        private bool IsNext(char character)
        {
            if (IsAtTheEnd) return false;
            if (character != _input[_current]) return false;
            _current++;
            return true;
        }
    }
}
