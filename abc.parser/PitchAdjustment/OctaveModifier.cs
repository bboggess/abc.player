using abc.parser.model;

namespace abc.parser.PitchAdjustment;

/// <summary>
/// Modifies a pitch by shifting the octave up or down.
/// </summary>
public class OctaveModifier : IPitchAdjuster
{
    private const char OctaveUpToken = '\'';
    private const char OctaveDownToken = ',';

    private readonly int _adjustment;

    private OctaveModifier(int adjustment)
    {
        _adjustment = adjustment;
    }

    /// <summary>
    /// Parses an OctaveModifier from the ABC octave string (a series of ' or ,).
    /// </summary>
    /// <param name="octaveDescriptor">The string specified in the ABC file</param>
    /// <returns>An object that will appropriately shift up or down the octave</returns>
    /// <exception cref="ArgumentNullException"><paramref name="octaveDescriptor"/> is null</exception>
    /// <exception cref="ArgumentException"><paramref name="octaveDescriptor"/> does not follow the ABC spec</exception>
    public static OctaveModifier FromDescriptor(string octaveDescriptor)
    {
        if (octaveDescriptor is null)
        {
            throw new ArgumentNullException(nameof(octaveDescriptor));
        }

        if (string.IsNullOrEmpty(octaveDescriptor))
        {
            return new OctaveModifier(0);
        }

        if (octaveDescriptor.All(c => c == OctaveUpToken))
        {
            return new OctaveModifier(octaveDescriptor.Length);
        }

        if (octaveDescriptor.All(c => c == OctaveDownToken))
        {
            return new OctaveModifier(-octaveDescriptor.Length);
        }

        throw new ArgumentException(
            $"Octave descriptor must be string of {OctaveUpToken} or {OctaveDownToken}",
            nameof(octaveDescriptor)
        );
    }

    public Pitch AdjustPitch(Pitch pitch)
    {
        return pitch.ChangeOctave(_adjustment);
    }
}
