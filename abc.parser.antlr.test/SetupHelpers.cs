using Antlr4.Runtime;

namespace abc.parser.antlr.test;

/// <summary>
/// Generic setup code for unit tests in any grammar to use.
/// </summary>
internal class SetupHelpers
{
    /// <summary>
    /// Builds up the parser from file content, allowing you to set up the
    /// parse tree at a smaller scope than the entire file. You need to manually create a parse tree from here.
    /// 
    /// Uses the <see cref="BailErrorStrategy"/> to handle errors, meaning an exception will be thrown on parse error.
    /// 
    /// This is generic over the grammar you want to use. You need to
    /// make sure that <typeparamref name="TLexer"/> and <typeparamref name="TParser"/>
    /// are both associated with the same grammar or things will break.
    /// </summary>
    /// <typeparam name="TLexer">The concrete lexer for your grammar</typeparam>
    /// <typeparam name="TParser">The concrete parser for your grammar</typeparam>
    /// <param name="testContent">The ABC content to test</param>
    /// <param name="lexerFactory">A function capable of constructing a new <typeparamref name="TLexer"/></param>
    /// <param name="parserFactory">A function capable of constructing a new <typeparamref name="TParser"/></param>
    /// <returns>Parser that can be used to build the parse tree</returns>
    public static TParser SetUpParser<TLexer, TParser>(string testContent, Func<ICharStream, TLexer> lexerFactory, Func<ITokenStream, TParser> parserFactory)
        where TLexer : Lexer
        where TParser : Parser 
    {
        var inputStream = new AntlrInputStream(testContent);
        var lexer = lexerFactory(inputStream);
        var tokens = new CommonTokenStream(lexer);

        var parser = parserFactory(tokens);
        parser.ErrorHandler = new BailErrorStrategy();

        return parser;
    }
}
