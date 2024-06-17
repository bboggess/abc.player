using abc.parser.parse;
using Antlr4.Runtime.Tree;

namespace abc.parser.antlr;

/// <summary>
/// Parses the body of an ABC tune. Walks through the entire body, sending
/// messages to the domain layer as discrete musical entities are parsed.
/// </summary>
public class TuneBodyParser : ITuneProcessor
{
    private readonly AbcContextProvider _provider;

    /// <summary>
    /// Sets up a parser with the necessary Antlr context
    /// </summary>
    /// <param name="provider">Provides the context of an Antlr parse tree</param>
    public TuneBodyParser(AbcContextProvider provider)
    {
        if (provider is null)
        {
            throw new ArgumentNullException(nameof(provider));
        }

        _provider = provider;
    }

    public void Process(ITuneBodyParser parser)
    {
        var bodyContext = _provider.GetBodyContext();

        var walker = new ParseTreeWalker();
        var listener = new TuneBodyListener(parser);

        walker.Walk(listener, bodyContext);
    }
}
