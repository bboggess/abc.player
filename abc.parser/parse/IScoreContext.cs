using abc.parser.model;

namespace abc.parser.parse;

/// <summary>
/// Tracks the context needed to decipher written notes in a tune body.
/// This could mean, for example, key signature, accidentals, or default note length.
/// </summary>
public interface IScoreContext
{
    /// <summary>
    /// Given a written natural note, adjusts the pitch as appropriate
    /// for the current point in the tune.
    /// </summary>
    /// <param name="pitch">The basic pitch, as written.</param>
    /// <returns>A new pitch with all key and accidental considerations applied.</returns>
    Pitch ResolvePitch(Pitch pitch);

    /// <summary>
    /// Given the duration applied to a note, translate that into the actual note length.
    /// </summary>
    /// <param name="duration">The relative duration given in the score.</param>
    /// <returns>The note length as a ratio (so 1/4 is a quarter note)</returns>
    Ratio ResolveDuration(DurationDescription duration);

    /// <summary>
    /// Records that an accidental has been marked. This allows us to persist accidentals
    /// through a measure.
    /// </summary>
    /// <param name="accidental">The accidental that's been applied.</param>
    void AddNewAccidental(BodyAccidental accidental);
}
