using abc.parser.model;

namespace abc.parser.PitchAdjustment;

/// <summary>
/// A targetted pitch adjuster has the ability to only adjust specific notes,
/// leaving all others alone.
/// </summary>
public interface ITargetedPitchAdjuster : IPitchAdjuster
{
    bool IsTargetingPitch(Pitch pitch);
}
