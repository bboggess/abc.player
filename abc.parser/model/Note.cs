namespace abc.parser.model;

public enum BaseNote
{
    A,
    B,
    C,
    D,
    E,
    F,
    G,
}

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
public class Note
{
    public BaseNote BaseNote { get; }
    public Accidental Accidental { get; }

    public Note(BaseNote note, Accidental accidental)
    {
        BaseNote = note;
        Accidental = accidental;
    }
}
