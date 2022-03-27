using InterpreterLib.Lexer;

Lexer lexer = new Lexer(LoadSourceCode(@"..\..\..\input.txt"));
List<Token> tokens = lexer.Tokenize();
tokens.ForEach(token => Console.WriteLine(token));

string LoadSourceCode(string fileName)
{
    if (!File.Exists(fileName)) return "";
    return File.ReadAllText(fileName);
}