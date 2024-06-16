using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Visits the full header, building a tune header object as we go.
/// </summary>
internal class FullHeaderVisitor : AbcBaseVisitor<ITuneHeaderBuilder>
{
    private ITuneHeaderBuilder _builder;

    /// <summary>
    /// Constructs our visitor using the builder you've provided. Note that visiting will return a new
    /// builder, so you must use the builder return from Visit to build your header. If you try to build
    /// from the same builder instance you passed in, you will have unexpectedly blank fields.
    /// </summary>
    /// <param name="builder">
    /// As we parse each field, that data will be registered with this builder. This is the builder we start with,
    /// but we will return a new builder to use after visiting.
    /// </param>
    public FullHeaderVisitor(ITuneHeaderBuilder builder)
    {
        _builder = builder;
    }

    public override ITuneHeaderBuilder VisitFieldNumber(
        [NotNull] AbcParser.FieldNumberContext context
    )
    {
        _builder = _builder.WithTrackIndex(new IndexVisitor().Visit(context));

        return _builder;
    }

    public override ITuneHeaderBuilder VisitFieldTitle(
        [NotNull] AbcParser.FieldTitleContext context
    )
    {
        _builder = _builder.WithTitle(new TrackTitleVisitor().Visit(context));

        return _builder;
    }

    public override ITuneHeaderBuilder VisitFieldKey([NotNull] AbcParser.FieldKeyContext context)
    {
        _builder = _builder.WithKey(new KeySignatureVisitor().Visit(context));

        return _builder;
    }

    public override ITuneHeaderBuilder VisitFieldComposer(
        [NotNull] AbcParser.FieldComposerContext context
    )
    {
        _builder = _builder.WithComposer(new ComposerVisitor().Visit(context));

        return _builder;
    }

    public override ITuneHeaderBuilder VisitFieldLength(
        [NotNull] AbcParser.FieldLengthContext context
    )
    {
        _builder = _builder.WithDefaultNoteLength(new BaseNoteLengthVisitor().Visit(context));

        return _builder;
    }

    public override ITuneHeaderBuilder VisitFieldMeter(
        [NotNull] AbcParser.FieldMeterContext context
    )
    {
        _builder = _builder.WithMeter(new MeterVisitor().Visit(context));

        return _builder;
    }

    public override ITuneHeaderBuilder VisitFieldTempo(
        [NotNull] AbcParser.FieldTempoContext context
    )
    {
        _builder = _builder.WithTempo(new TempoVisitor().Visit(context));

        return _builder;
    }
}
