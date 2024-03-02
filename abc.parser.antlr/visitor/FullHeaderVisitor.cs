using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Visits the full header, building a tune header object as we go.
/// </summary>
internal class FullHeaderVisitor : AbcHeaderBaseVisitor<ITuneHeaderBuilder>
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
        [NotNull] AbcHeaderParser.FieldNumberContext context
    )
    {
        _builder = _builder.WithTrackIndex(new IndexVisitor().Visit(context));

        return _builder;
    }

    public override ITuneHeaderBuilder VisitFieldTitle(
        [NotNull] AbcHeaderParser.FieldTitleContext context
    )
    {
        _builder = _builder.WithTitle(new TrackTitleVisitor().Visit(context));

        return _builder;
    }

    public override ITuneHeaderBuilder VisitFieldKey(
        [NotNull] AbcHeaderParser.FieldKeyContext context
    )
    {
        _builder = _builder.WithKey(new KeySignatureVisitor().Visit(context));

        return _builder;
    }

    public override ITuneHeaderBuilder VisitFieldComposer(
        [NotNull] AbcHeaderParser.FieldComposerContext context
    )
    {
        _builder = _builder.WithComposer(new ComposerVisitor().Visit(context));

        return _builder;
    }

    public override ITuneHeaderBuilder VisitFieldLength(
        [NotNull] AbcHeaderParser.FieldLengthContext context
    )
    {
        _builder = _builder.WithDefaultNoteLength(new BaseNoteLengthVisitor().Visit(context));

        return _builder;
    }

    public override ITuneHeaderBuilder VisitFieldMeter(
        [NotNull] AbcHeaderParser.FieldMeterContext context
    )
    {
        _builder = _builder.WithMeter(new MeterVisitor().Visit(context));

        return _builder;
    }

    public override ITuneHeaderBuilder VisitFieldTempo(
        [NotNull] AbcHeaderParser.FieldTempoContext context
    )
    {
        _builder = _builder.WithTempo(new TempoVisitor().Visit(context));

        return _builder;
    }
}
