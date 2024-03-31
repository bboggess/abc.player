namespace abc.parser.model;

/// <summary>
/// Represents one of the twelve notes of the chromatic scale. Use the static members
/// to access instances of this class.
/// </summary>
public class BaseNote
{
    internal const int NumChromaticNotes = 12;

    public static readonly BaseNote A = new(0);
    public static readonly BaseNote ASharp = new(1);
    public static readonly BaseNote B = new(2);
    public static readonly BaseNote C = new(3);
    public static readonly BaseNote CSharp = new(4);
    public static readonly BaseNote D = new(5);
    public static readonly BaseNote DSharp = new(6);
    public static readonly BaseNote E = new(7);
    public static readonly BaseNote F = new(8);
    public static readonly BaseNote FSharp = new(9);
    public static readonly BaseNote G = new(10);
    public static readonly BaseNote GSharp = new(11);

    private readonly int _value;

    private BaseNote(int value)
    {
        _value = (value % NumChromaticNotes + NumChromaticNotes) % NumChromaticNotes;
    }

    /// <summary>
    /// Whether this note is one of the 7 natural notes.
    /// </summary>
    public bool IsNatural =>
        this.Equals(A)
        || this.Equals(B)
        || this.Equals(C)
        || this.Equals(D)
        || this.Equals(E)
        || this.Equals(F)
        || this.Equals(G);

    /// <summary>
    /// Shift the note by a number of semitones. This "wraps", so passing 12 returns the same note.
    /// </summary>
    /// <param name="numSemitones">Up is positive, down is negative.</param>
    /// <returns>A new note shifted up or down by <paramref name="numSemitones"/></returns>
    public BaseNote Transpose(int numSemitones)
    {
        return new BaseNote(_value + numSemitones);
    }

    /// <summary>
    /// Return a new note with the given accidental applied.
    /// </summary>
    /// <param name="accidental">The accidental you're applying. Natural just means "do nothing"</param>
    /// <returns>
    /// A new note shifted up one if passed in sharp, down one if passed in flat, or an equivalent note if passed natural.
    /// </returns>
    public BaseNote ApplyAccidental(Accidental accidental)
    {
        var shift = accidental switch
        {
            Accidental.Flat => -1,
            Accidental.Sharp => 1,
            _ => 0,
        };

        return Transpose(shift);
    }

    /// <summary>
    /// How far after this note <paramref name="other"/> comes.
    ///
    /// This assumes an ordering, but as long as you're using this method,
    /// that ordering is free to change without affecting consumers.
    /// </summary>
    /// <param name="other">The pivot note. We're looking at positioning relative to what you pass in.</param>
    /// <returns>
    /// Where this note is relative to <paramref name="other"/>. Negative means <paramref name="other"/> comes after.
    /// </returns>
    public int DistanceFrom(BaseNote other)
    {
        return _value - other._value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is BaseNote other)
        {
            return _value == other._value;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return _value;
    }

    /// <summary>
    /// Constructs a (natural) note from the name of the note. Will never return
    /// anything with accidentals.
    /// </summary>
    /// <param name="c">The name of the note, e.g. 'A', 'B', etc.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="c"/> is not a valid note name</exception>
    public static BaseNote FromChar(char c)
    {
        return c switch
        {
            'A' => A,
            'B' => B,
            'C' => C,
            'D' => D,
            'E' => E,
            'F' => F,
            'G' => G,
            _ => throw new ArgumentOutOfRangeException(nameof(c)),
        };
    }

    public static IEnumerable<BaseNote> AllNotes()
    {
        return new[] { A, ASharp, B, C, CSharp, D, DSharp, E, F, FSharp, G, GSharp };
    }
}
