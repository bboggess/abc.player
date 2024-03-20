using abc.parser.model;
using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Parse the key signature from an ABC header.
/// </summary>
public class KeySignatureVisitor : AbcHeaderBaseVisitor<KeySignature>
{
    public override KeySignature VisitFieldKey([NotNull] AbcHeaderParser.FieldKeyContext context)
    {
        return Visit(context.keySignature());
    }

    public override KeySignature VisitKeySignature(
        [NotNull] AbcHeaderParser.KeySignatureContext context
    )
    {
        var mode = context.modeKey() is null
            ? Mode.Major
            : new KeyModeVisitor().Visit(context.modeKey());
        var accidental = context.accidentalKey() is null
            ? Accidental.Natural
            : new KeyAccidentalVisitor().Visit(context.accidentalKey());
        var basePitch = BaseNote.FromChar(context.note().GetText().First());
        return new KeySignature(new KeyTonic(basePitch, accidental), mode);
    }
}

/// <summary>
/// Parses the mode portion of a key signature. Currently minor and major are all we support.
/// </summary>
internal class KeyModeVisitor : AbcHeaderBaseVisitor<Mode>
{
    public override Mode VisitMinor([NotNull] AbcHeaderParser.MinorContext context)
    {
        return Mode.Minor;
    }
}

/// <summary>
/// Parses the accidental attached to a key signature (e.g. the # in F#).
/// </summary>
internal class KeyAccidentalVisitor : AbcHeaderBaseVisitor<Accidental>
{
    public override Accidental VisitSharp([NotNull] AbcHeaderParser.SharpContext context)
    {
        return Accidental.Sharp;
    }

    public override Accidental VisitFlat([NotNull] AbcHeaderParser.FlatContext context)
    {
        return Accidental.Flat;
    }
}
