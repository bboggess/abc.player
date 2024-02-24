namespace abc.parser.antlr;

/// <summary>
/// Defines the tempo at which a piece should be played back
/// </summary>
public class TempoDefinition
{
    private readonly Ratio _pulse;
    private readonly double _beatsPerMinute;

    /// <summary>
    /// Assuming a quarter note pulse, gets the number of beats per minute.
    /// </summary>
    public double QuarterNotePerMinute
    {
        get
        {
            return ConvertBpmToPulse(new Ratio(1, 4));
        }
    }

    private double ConvertBpmToPulse(Ratio newPulse)
    {
        var conversionRatio = new Ratio(_pulse, newPulse);
        return conversionRatio.ToDecimal() * _beatsPerMinute;
    }

    /// <summary>
    /// Build a tempo from a given pulse and the frequency of that pulse.
    /// </summary>
    /// <param name="pulse">The beat that gets the pulse, as a fraction. So 1/4 is quarter note beat, 1/2 is half note, etc.</param>
    /// <param name="beatsPerMinute">The number of the given pulse that occur per minute</param>
    public TempoDefinition(Ratio pulse, int beatsPerMinute)
    {
        _pulse = pulse;
        _beatsPerMinute = beatsPerMinute;
    }
}
