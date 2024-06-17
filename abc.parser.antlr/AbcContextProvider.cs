using Antlr4.Runtime;

namespace abc.parser.antlr;

/// <summary>
/// Provides the header and body Antlr contexts for an ABC file.
/// </summary>
public class AbcContextProvider
{
    private readonly AbcParser _parser;

    /// <summary>
    /// Context injector for an ABC file.
    /// </summary>
    /// <param name="input">This is the actual contents of the ABC file</param>
    /// <param name="errorStrategy">Determines how we should handle syntax error during the parse</param>
    public AbcContextProvider(ICharStream input, IAntlrErrorStrategy errorStrategy)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        if (errorStrategy is null)
        {
            throw new ArgumentNullException(nameof(errorStrategy));
        }

        _parser = ConstructParser(input, errorStrategy);
    }

    public AbcParser.TuneHeaderContext GetHeaderContext()
    {
        return _parser.tuneHeader();
    }

    public AbcParser.TuneBodyContext GetBodyContext()
    {
        return _parser.tuneBody();
    }

    private static AbcParser ConstructParser(ICharStream input, IAntlrErrorStrategy errorStrategy)
    {
        var lexer = new AbcLexer(input);
        var tokens = new CommonTokenStream(lexer);
        return new AbcParser(tokens) { ErrorHandler = errorStrategy };
    }
}
