using abc.parser.model;
using abc.parser.parse;
using abc.parser.PitchAdjustment;

namespace abc.parser;

/// <summary>
/// Tracks the current musical context of where we are in the tune.
/// </summary>
public class ScoreContext : IScoreContext
{
    private IPitchAdjuster _pitchAdjuster;
    private readonly Ratio _baseNoteLength;

    public ScoreContext(TuneHeader header)
    {
        _pitchAdjuster = header.Key.ToAccidentalCorrector();
        _baseNoteLength = header.BaseNoteLength;
    }

    public void AddNewAccidental(BodyAccidental accidental)
    {
        _pitchAdjuster = new PitchAdjusterWithBackup(accidental.ToPitchAdjuster(), _pitchAdjuster);
    }

    public Ratio ResolveDuration(DurationDescription duration)
    {
        return duration.Multiplier.Multiply(_baseNoteLength);
    }

    public Pitch ResolvePitch(Pitch pitch)
    {
        return _pitchAdjuster.AdjustPitch(pitch);
    }
}
