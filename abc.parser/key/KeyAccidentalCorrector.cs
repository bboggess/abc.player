using abc.parser.model;

namespace abc.parser.key;

/// <summary>
/// Applies key signature accidentals to pitches.
/// </summary>
internal class KeyAccidentalCorrector : IPitchAdjuster
{
    private readonly int _pitchAdjustment;
    private readonly ISet<BaseNote> _accidentals;

    public KeyAccidentalCorrector(AccidentalCount count)
    {
        _pitchAdjustment = count.Type switch
        {
            AccidentalType.Sharp => 1,
            AccidentalType.Flat => -1,
            _
                => throw new ArgumentException(
                    $"Accidental type ${count.Type} is not a valid enum value",
                    nameof(count)
                ),
        };

        _accidentals = AccidentalStepper
            .FromAccidentalType(count.Type)
            .GetAccidentals(count.NumAccidentals);
    }

    public Pitch AdjustPitch(Pitch pitch)
    {
        return _accidentals.Contains(pitch.ChromaticNote) ? pitch.Adjust(_pitchAdjustment) : pitch;
    }
}
