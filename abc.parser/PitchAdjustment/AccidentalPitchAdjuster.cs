using abc.parser.model;

namespace abc.parser.PitchAdjustment;

/// <summary>
/// A pitch adjuster that is meant to step a specific note
/// up or down. Will only affect the specified note.
/// </summary>
public class AccidentalPitchAdjuster : ITargetedPitchAdjuster
{
    private readonly BaseNote _target;
    private readonly int _delta;

    /// <summary>
    /// Creates an object to adjust a note sharp or flat.
    /// </summary>
    /// <param name="note">The note to target</param>
    /// <param name="delta">The number of half steps to adjust by</param>
    public AccidentalPitchAdjuster(BaseNote note, int delta)
    {
        _target = note;
        _delta = delta;
    }

    public Pitch AdjustPitch(Pitch pitch)
    {
        if (pitch is null)
        {
            throw new ArgumentNullException(nameof(pitch));
        }

        return IsTargetingPitch(pitch) ? pitch.Transpose(_delta) : pitch;
    }

    public bool IsTargetingPitch(Pitch pitch)
    {
        return pitch.ChromaticNote.Equals(_target);
    }
}
