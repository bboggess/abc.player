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
    public NaturalNote BaseNaturalNote { get; }
    public Accidental Accidental { get; }

    /// <summary>
    /// Returns the tonic as a <see cref="BaseNote"/>. This throws away information
    /// about flats vs sharps, so is not always what you want.
    /// </summary>
    public BaseNote TonicPitch => (BaseNote)(((int)BaseNaturalNote + GetAccidentalModifier()) % 12);

    private int GetAccidentalModifier()
    {
        return Accidental switch
        {
            Accidental.Flat => -1,
            Accidental.Sharp => 1,
            _ => 0,
        };
    }

    public KeyTonic(NaturalNote note, Accidental accidental)
    {
        BaseNaturalNote = note;
        Accidental = accidental;
    }
}
