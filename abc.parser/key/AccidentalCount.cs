using abc.parser.model;

namespace abc.parser.key;

internal class AccidentalCount
{
    /// <summary>
    /// Number of accidentals, guaranteed to be in the range [0,8).
    /// </summary>
    public int NumAccidentals { get; }

    /// <summary>
    /// Whether this is counting sharps or flats
    /// </summary>
    public AccidentalType Type { get; }

    /// <summary>
    /// Construct an accidental count.
    /// </summary>
    /// <param name="numAccidentals">The number of accidentals needed</param>
    /// <param name="type">whether those accidentals are sharps or flats</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="numAccidentals"/> is negative or greater than 7</exception>
    public AccidentalCount(int numAccidentals, AccidentalType type)
    {
        if (numAccidentals < 0 || numAccidentals > KeySignature.MaxAccidentals)
        {
            throw new ArgumentOutOfRangeException(nameof(numAccidentals));
        }

        NumAccidentals = numAccidentals;
        Type = type;
    }
}

/// <summary>
/// Different types of accidentals we might count. Does not include natural, since this
/// is meant for cases where we are guaranteed to have an accidental.
/// </summary>
internal enum AccidentalType
{
    Sharp,
    Flat
}
