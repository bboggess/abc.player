namespace abc.parser.model;

/// <summary>
/// Represents a pitch that can be played, without reference to how long it should be
/// held.
/// </summary>
public class Pitch
{
    internal const int NumChromaticNotes = 12;

    // using MIDI values to represent pitch
    private const int _middleC = 60;
    private const int _minMidiValue = 0;
    private const int _maxMidiValue = 127;

    private readonly int _midiValue;

    /// <summary>
    /// Construct a pitch from description of a note
    /// </summary>
    /// <param name="note">One of the 12 chromatic tones</param>
    /// <param name="octaveFromMiddleC">
    /// Which octave the pitch should play in, relative to middle C.
    /// E.g., to describe the A below middle C, pass in 0 here.
    /// </param>
    public Pitch(BaseNote note, int octaveFromMiddleC)
        : this(_middleC + octaveFromMiddleC * NumChromaticNotes + ((int)note - (int)BaseNote.C)) { }

    private Pitch(int noteValue)
    {
        if (noteValue < _minMidiValue || noteValue > _maxMidiValue)
        {
            throw new ArgumentOutOfRangeException(nameof(noteValue));
        }

        _midiValue = noteValue;
    }

    public BaseNote NamedNote => (BaseNote)((_midiValue - (int)BaseNote.C) % NumChromaticNotes);

    /// <summary>
    /// Adjust a pitch up or down by specified number of half steps.
    /// </summary>
    /// <param name="numHalfSteps">Number of half steps to adjust. Negative means flatten.</param>
    /// <returns>A new Pitch <paramref name="numHalfSteps"/> half steps from this one</returns>
    public Pitch Adjust(int numHalfSteps)
    {
        return new Pitch(_midiValue + numHalfSteps);
    }
}

public enum BaseNote
{
    A = 0,
    ASharp = 1,
    B = 2,
    C = 3,
    CSharp = 4,
    D = 5,
    DSharp = 6,
    E = 7,
    F = 8,
    FSharp = 9,
    G = 10,
    GSharp = 11,
}

public enum NaturalNote
{
    A = BaseNote.A,
    B = BaseNote.B,
    C = BaseNote.C,
    D = BaseNote.D,
    E = BaseNote.E,
    F = BaseNote.F,
    G = BaseNote.G,
}
