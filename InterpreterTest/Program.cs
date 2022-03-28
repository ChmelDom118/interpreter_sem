using InterpreterLib.Lexer;
using InterpreterLib.Parser;

Lexer lexer = new Lexer(LoadSourceCode(@"..\..\..\input.txt"));
List<Token> tokens = lexer.Tokenize();
tokens.ForEach(token => Console.WriteLine(token));

Parser parser = new Parser(tokens);
InterpreterLib.AST.Program program = parser.Parse();
Console.ReadKey();

string LoadSourceCode(string fileName)
{
    if (!File.Exists(fileName)) return "";
    return File.ReadAllText(fileName);
}