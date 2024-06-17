using abc.parser.exception;
using abc.parser.key;
using abc.parser.PitchAdjustment;

namespace abc.parser.model;

public enum Mode
{
    Major,
    Minor,
}

/// <summary>
/// Simple model for a key signature parsed from an ABC file.
/// </summary>
public class KeySignature
{
    internal const int MaxAccidentals = 7;

    public KeyTonic Tonic { get; }
    public Mode Mode { get; }

    public KeySignature(KeyTonic tonic, Mode mode)
    {
        Tonic = tonic;
        Mode = mode;
    }

    /// <summary>
    /// Builds an object that can be used to apply the key signature to pitches.
    /// </summary>
    /// <returns>an adjuster that will adjust pitches to fit the key signature</returns>
    /// <exception cref="InvalidKeyException">the key has double accidentals</exception>
    public IPitchAdjuster ToAccidentalCorrector()
    {
        var counter = AccidentalCounterFactory.GetAccidentalCounter(Mode, Tonic.Accidental);
        var result = counter.CountAccidentals(
            Tonic.BaseNaturalNote.ApplyAccidental(Tonic.Accidental)
        );

        if (!result.TryGetValue(out AccidentalCount? count))
        {
            // Invalid key was provided to us, perhaps something like D# major.
            throw new InvalidKeyException(Tonic, Mode);
        }

        return new KeyAccidentalCorrector(count!);
    }
}
