namespace abc.parser.model;

public enum Accidental
{
    Natural,
    Flat,
    Sharp,
}

/// <summary>
/// Simple description of a note, meant to be used in a key signature. Note
/// this means that Gb and F# are meaningfully different.
/// </summary>
public class KeyTonic
{
    public NaturalNote BaseNote { get; }
    public Accidental Accidental { get; }

    public KeyTonic(NaturalNote note, Accidental accidental)
    {
        BaseNote = note;
        Accidental = accidental;
    }
}
