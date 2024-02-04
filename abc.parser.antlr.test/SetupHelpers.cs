using Antlr4.Runtime.Tree;
using Antlr4.Runtime;

namespace abc.parser.antlr.test;

/// <summary>
/// Shared setup code for unit tests.
/// 
/// As a note, we are preferring helpers over framework level
/// setup methods.
/// </summary>
internal class SetupHelpers
{
    /// <summary>
    /// Builds up a parse tree from the file content we want to test.
    /// </summary>
    /// <param name="testContent">
    /// String containing the ABC file content to test with. This needs to be
    /// formatted exactly as an ABC file would be, with newlines and all.
    /// </param>
    /// <returns>Parse tree that can be walked.</returns>
    public static IParseTree SetUpParseTree(string testContent)
    {
        var inputStream = new AntlrInputStream(testContent);
        var lexer = new ABCLexer(inputStream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new ABCParser(tokens);

        return parser.abcFile();
    }

    /// <summary>
    /// Builds up the parser from file content, allowing you to attach error listeners
    /// to catch potential parse errors.
    /// 
    /// This does not actually attempt to create the parse tree, you must do that
    /// manually.
    /// </summary>
    /// <param name="testContent">
    /// String containing the ABC file content to test with. This needs to be
    /// formatted exactly as an ABC file would be, with newlines and all.
    /// </param>
    /// <param name="errorListener">An error listener to attach. Will replace the default.</param>
    /// <returns>A parser you can then use to build a parse tree</returns>
    public static ABCParser SetUpParser(string testContent, IAntlrErrorListener<IToken> errorListener)
    {
        var inputStream = new AntlrInputStream(testContent);
        var lexer = new ABCLexer(inputStream);
        var tokens = new CommonTokenStream(lexer);

        var parser = new ABCParser(tokens);
        parser.RemoveErrorListeners();
        parser.AddErrorListener(errorListener);

        return parser;
    }
}
