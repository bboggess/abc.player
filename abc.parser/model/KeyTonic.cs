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
    public BaseNote BaseNaturalNote { get; }
    public Accidental Accidental { get; }

    /// <summary>
    /// Builds a Tonic by specifying the natural piece and accidental piece separately.
    ///
    /// E.g., to describe F#, you'd pass in BaseNote.F and Accidental.Sharp.
    /// </summary>
    /// <param name="note">This needs to be a natural note.</param>
    /// <param name="accidental">Applies an accidental to <paramref name="note"/> (or natural to use no accidental)</param>
    /// <exception cref="ArgumentException"><paramref name="note"/> is not natural</exception>
    public KeyTonic(BaseNote note, Accidental accidental)
    {
        if (!note.IsNatural)
        {
            throw new ArgumentException(
                $"Must provide a natural note, received {note}",
                nameof(note)
            );
        }

        BaseNaturalNote = note;
        Accidental = accidental;
    }
}
