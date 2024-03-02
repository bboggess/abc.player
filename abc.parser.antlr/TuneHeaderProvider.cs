using abc.parser.antlr.visitor;
using abc.parser.model;

namespace abc.parser.antlr;

/// <summary>
/// Parses the header of an ABC file using an Antlr grammar.
/// </summary>
public class TuneHeaderProvider : ITuneHeaderProvider
{
    private readonly IHeaderContextProvider _contextProvider;

    /// <summary>
    /// Constructs our provider from the context of a header parse tree.
    /// </summary>
    /// <param name="contextProvider">The parsing context for the source of the header, e.g. from a file</param>
    /// <exception cref="ArgumentNullException"><paramref name="contextProvider"/> is null</exception>
    public TuneHeaderProvider(IHeaderContextProvider contextProvider)
    {
        if (contextProvider is null)
        {
            throw new ArgumentNullException(nameof(contextProvider));
        }

        _contextProvider = contextProvider;
    }

    public TuneHeader GetTuneHeader()
    {
        var context = _contextProvider.GetHeaderContext();
        var builder = TuneHeader.Builder();

        var visitor = new FullHeaderVisitor(builder);
        return visitor.Visit(context).Build();
    }
}
