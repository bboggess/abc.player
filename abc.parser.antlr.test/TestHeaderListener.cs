using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.test;

/// <summary>
/// Tests the definition of our header grammar. When the parse tree is walked,
/// will store each header field's value in a (possibly null) property.
/// 
/// No special handling is done to further process values, with the exception
/// of converting purely numeric fields (e.g. track ID) into ints. All other
/// fields will simply have the raw text found in the track header.
/// </summary>
internal class TestHeaderListener : ABCBaseListener
{
    public string? Title { get; private set; }

    public int TrackNum { get; private set; }

    public string? Meter { get; private set; }

    public string? Key { get; private set; }

    public string? TempoDesc { get; private set; }

    public string? Composer { get; private set; }

    public string? DefaultNoteLength { get; private set; }

    public override void EnterFieldTitle([NotNull] ABCParser.FieldTitleContext context)

    {
        Title = context.text().GetText();
    }

    public override void EnterFieldNumber([NotNull] ABCParser.FieldNumberContext context)
    {
        TrackNum = int.Parse(context.INT().GetText());
    }

    public override void EnterFieldMeter([NotNull] ABCParser.FieldMeterContext context)
    {
        Meter = context.timeSignature().GetText();
    }

    public override void EnterFieldKey([NotNull] ABCParser.FieldKeyContext context)
    {
        Key = context.keySignature().GetText();
    }

    public override void EnterFieldTempo([NotNull] ABCParser.FieldTempoContext context)
    {
        TempoDesc = context.tempoDef().GetText();
    }

    public override void EnterFieldComposer([NotNull] ABCParser.FieldComposerContext context)
    {
        Composer = context.text().GetText();
    }

    public override void EnterFieldLength([NotNull] ABCParser.FieldLengthContext context)
    {
        DefaultNoteLength = context.fraction().GetText();
    }
}
