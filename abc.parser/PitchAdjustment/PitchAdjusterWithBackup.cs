using abc.parser.model;

namespace abc.parser.PitchAdjustment;

/// <summary>
/// Defines a preferred, targetted pitch adjustment. If the given pitch isn't being
/// targetted, then falls back to another generic pitch adjuster.
/// </summary>
public class PitchAdjusterWithBackup : IPitchAdjuster
{
    private readonly ITargetedPitchAdjuster _preferredAdjuster;
    private readonly IPitchAdjuster _backupAdjuster;

    /// <summary>
    /// Specifies the preferred and fallback adjusters to use.
    /// </summary>
    /// <param name="preferredAdjuster">
    /// We will always attempt to run this adjustment first. If this is targetting the pitch,
    /// the backup will not be run.
    /// </param>
    /// <param name="backupAdjuster">
    /// If the preferred is not targetting the pitch, this will be run instead.
    /// </param>
    /// <exception cref="ArgumentNullException">Either argument is null</exception>
    public PitchAdjusterWithBackup(
        ITargetedPitchAdjuster preferredAdjuster,
        IPitchAdjuster backupAdjuster
    )
    {
        if (preferredAdjuster is null)
        {
            throw new ArgumentNullException(nameof(preferredAdjuster));
        }

        if (backupAdjuster is null)
        {
            throw new ArgumentNullException(nameof(backupAdjuster));
        }

        _preferredAdjuster = preferredAdjuster;
        _backupAdjuster = backupAdjuster;
    }

    public Pitch AdjustPitch(Pitch pitch)
    {
        if (_preferredAdjuster.IsTargetingPitch(pitch))
        {
            return _preferredAdjuster.AdjustPitch(pitch);
        }

        return _backupAdjuster.AdjustPitch(pitch);
    }
}
