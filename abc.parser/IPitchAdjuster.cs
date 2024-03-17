using abc.parser.model;

namespace abc.parser;

/// <summary>
/// An interface for applying adjustments to a given pitch.
/// </summary>
public interface IPitchAdjuster
{
    /// <summary>
    /// Adjusts the given pitch in some way, returning a new pitch.
    /// If no changes need to be made, may return the instance passed in.
    /// </summary>
    Pitch AdjustPitch(Pitch pitch);
}
