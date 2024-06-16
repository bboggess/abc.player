using abc.parser.model;
using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Parse the key signature from an ABC header.
/// </summary>
public class KeySignatureVisitor : AbcBaseVisitor<KeySignature>
{
    public override KeySignature VisitFieldKey([NotNull] AbcParser.FieldKeyContext context)
    {
        return Visit(context.keySignature());
    }

    public override KeySignature VisitKeySignature([NotNull] AbcParser.KeySignatureContext context)
    {
        var mode = context.modeKey() is null
            ? Mode.Major
            : new KeyModeVisitor().Visit(context.modeKey());
        var accidental = context.accidentalKey() is null
            ? Accidental.Natural
            : new KeyAccidentalVisitor().Visit(context.accidentalKey());
        var basePitch = BaseNote.FromChar(context.keyNote().GetText().First());
        return new KeySignature(new KeyTonic(basePitch, accidental), mode);
    }
}

/// <summary>
/// Parses the mode portion of a key signature. Currently minor and major are all we support.
/// </summary>
internal class KeyModeVisitor : AbcBaseVisitor<Mode>
{
    public override Mode VisitMinor([NotNull] AbcParser.MinorContext context)
    {
        return Mode.Minor;
    }
}

/// <summary>
/// Parses the accidental attached to a key signature (e.g. the # in F#).
/// </summary>
internal class KeyAccidentalVisitor : AbcBaseVisitor<Accidental>
{
    public override Accidental VisitSharp([NotNull] AbcParser.SharpContext context)
    {
        return Accidental.Sharp;
    }

    public override Accidental VisitFlat([NotNull] AbcParser.FlatContext context)
    {
        return Accidental.Flat;
    }
}
