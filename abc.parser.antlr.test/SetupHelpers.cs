using Antlr4.Runtime;

namespace abc.parser.antlr.test;

/// <summary>
/// Generic setup code for unit tests in any grammar to use.
/// </summary>
internal class SetupHelpers
{
    /// <summary>
    /// Builds up the parser from file content, allowing you to set up the parse tree at a smaller scope than the entire file.
    /// You need to manually create a parse tree from here.
    ///
    /// Uses the <see cref="BailErrorStrategy"/> to handle errors, meaning an exception will be thrown on parse error.
    /// are both associated with the same grammar or things will break.
    /// </summary>
    /// <param name="testContent">The ABC content to test</param>
    /// <returns>Parser that can be used to build the parse tree</returns>
    public static AbcParser SetUpParser(string testContent)
    {
        var inputStream = new AntlrInputStream(testContent);
        var lexer = new AbcLexer(inputStream);
        var tokens = new CommonTokenStream(lexer);

        var parser = new AbcParser(tokens) { ErrorHandler = new BailErrorStrategy() };

        return parser;
    }
}
