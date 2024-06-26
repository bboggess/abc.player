﻿using abc.parser.antlr.visitor;
using abc.parser.model;

namespace abc.parser.antlr;

/// <summary>
/// Parses the header of an ABC file using an Antlr grammar.
/// </summary>
public class TuneHeaderProvider : ITuneHeaderProvider
{
    private readonly ITuneHeaderBuilder _builder;
    private readonly AbcContextProvider _contextProvider;

    /// <summary>
    /// Constructs our provider from the context of a header parse tree.
    /// </summary>
    /// <param name="headerBuilder">Builder object for our header, used to set fields as we come across them.</param>
    /// <param name="contextProvider">The parsing context for the source of the header, e.g. from a file</param>
    /// <exception cref="ArgumentNullException">either argument is null</exception>
    public TuneHeaderProvider(ITuneHeaderBuilder headerBuilder, AbcContextProvider provider)
    {
        if (headerBuilder is null)
        {
            throw new ArgumentNullException(nameof(headerBuilder));
        }

        if (provider is null)
        {
            throw new ArgumentNullException(nameof(provider));
        }

        _builder = headerBuilder;
        _contextProvider = provider;
    }

    public TuneHeader GetTuneHeader()
    {
        var context = _contextProvider.GetHeaderContext();

        var visitor = new FullHeaderVisitor(_builder);
        return visitor.Visit(context).Build();
    }
}
