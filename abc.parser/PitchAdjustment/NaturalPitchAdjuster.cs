using abc.parser.model;
using abc.parser.PitchAdjustment;

namespace abc.parser;

/// <summary>
/// Pitch adjuster to use when a natural "accidental" has been applied.
///
/// Makes sure that the targeted note is always used without flats/sharps.
/// </summary>
public class NaturalPitchAdjuster : ITargetedPitchAdjuster
{
    private readonly BaseNote _target;

    public NaturalPitchAdjuster(BaseNote note)
    {
        _target = note;
    }

    public Pitch AdjustPitch(Pitch pitch)
    {
        return pitch;
    }

    public bool IsTargetingPitch(Pitch pitch)
    {
        return pitch.ChromaticNote.Equals(_target);
    }
}
