using System.Runtime.CompilerServices;

namespace abc.parser.model;

/// <summary>
/// Represents a pitch that can be played, without reference to how long it should be
/// held.
/// </summary>
public class Pitch
{
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
        : this(
            _middleC
                + octaveFromMiddleC * BaseNote.NumChromaticNotes
                + note.DistanceFrom(BaseNote.C)
        ) { }

    private Pitch(int noteValue)
    {
        if (noteValue < _minMidiValue || noteValue > _maxMidiValue)
        {
            throw new ArgumentOutOfRangeException(nameof(noteValue));
        }

        _midiValue = noteValue;
    }

    /// <summary>
    /// Represents the note from the chromatic scale underlying this pitch. Basically,
    /// this throws away octave information.
    /// </summary>
    public BaseNote ChromaticNote => BaseNote.C.Transpose(_midiValue - _middleC);

    /// <summary>
    /// Adjust a pitch up or down by specified number of half steps.
    /// </summary>
    /// <param name="numSemitones">Number of half steps to adjust. Negative means flatten.</param>
    /// <returns>A new Pitch <paramref name="numSemitones"/> half steps from this one</returns>
    public Pitch Transpose(int numSemitones)
    {
        return new Pitch(_midiValue + numSemitones);
    }
}
