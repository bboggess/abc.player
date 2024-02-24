using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Parse the key signature from an ABC header.
/// </summary>
public class KeySignatureVisitor : AbcHeaderBaseVisitor<KeySignature>
{
    public override KeySignature VisitKeySignature([NotNull] AbcHeaderParser.KeySignatureContext context)
    {
        var mode = context.modeKey() is null ? Mode.Major : new KeyModeVisitor().Visit(context.modeKey());
        var accidental = context.accidentalKey() is null ? Accidental.Natural : new KeyAccidentalVisitor().Visit(context.accidentalKey());
        var basePitch = NoteFromText(context.note().GetText()) ?? throw new ParseException(context);
        return new KeySignature(new Note(basePitch, accidental), mode);
    }

    private static BaseNote? NoteFromText(string s)
    {
        return s switch
        {
            "A" => BaseNote.A,
            "B" => BaseNote.B,
            "C" => BaseNote.C,
            "D" => BaseNote.D,
            "E" => BaseNote.E,
            "F" => BaseNote.F,
            "G" => BaseNote.G,
            _ => null,
        };
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