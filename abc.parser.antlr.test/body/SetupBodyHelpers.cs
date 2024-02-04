using Antlr4.Runtime;

namespace abc.parser.antlr.test.body;

/// <summary>
/// Shared setup code for body grammar unit tests.
/// </summary>
internal class SetupBodyHelpers
{
    private static readonly Func<ICharStream, AbcBodyLexer> LexerFactory = s => new AbcBodyLexer(s);
    private static readonly Func<ITokenStream, AbcBodyParser> ParserFactory = s => new AbcBodyParser(s);

    /// <summary>
    /// Builds up the parser from file content, allowing you to set up the
    /// parse tree at a smaller scope than the entire file.
    /// 
    /// You need to manually create a parse tree from here.
    /// </summary>
    /// <param name="testContent">
    /// String containing the ABC file content to test with. This needs to be
    /// formatted exactly as an ABC file would be, with newlines and all.
    /// </param>
    /// <returns>A parser you can then use to build a parse tree</returns>
    public static AbcBodyParser SetUpParser(string testContent)
    {
        return SetupHelpers.SetUpParser(testContent, LexerFactory, ParserFactory);
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
    public static AbcBodyParser SetUpParser(string testContent, IAntlrErrorListener<IToken> errorListener)
    {
        return SetupHelpers.SetUpParser(testContent, errorListener, LexerFactory, ParserFactory);
    }
}
