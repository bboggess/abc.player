using Antlr4.Runtime;

namespace abc.parser.antlr;

/// <summary>
/// An Antlr parser-generator for the tune header of an ABC file.
/// </summary>
public class HeaderParserGenerator : IHeaderContextProvider
{
    private readonly ICharStream _charStream;
    private readonly IAntlrErrorStrategy _errorStrategy;

    /// <summary>
    /// Construct a parser generator from text following the ABC header spec.
    /// </summary>
    /// <param name="input">The header text to parse as raw characters</param>
    /// <param name="errorStrategy">The strategy to use if parsing fails for any reason.</param>
    /// <exception cref="ArgumentNullException">If either argument is null</exception>
    public HeaderParserGenerator(ICharStream input, IAntlrErrorStrategy errorStrategy)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        if (errorStrategy is null)
        {
            throw new ArgumentNullException(nameof(errorStrategy));
        }

        _charStream = input;
        _errorStrategy = errorStrategy;
    }

    public AbcParser.TuneHeaderContext GetHeaderContext()
    {
        var lexer = new AbcLexer(_charStream);
        var tokens = new CommonTokenStream(lexer);
        var parser = new AbcParser(tokens) { ErrorHandler = _errorStrategy };

        return parser.tuneHeader();
    }
}
