namespace abc.parser.antlr.model;

/// <summary>
/// Represents time signature of a piece of music.
/// </summary>
public class TimeSignature
{
    /// <summary>
    /// The numerator when writing time signature as a fraction
    /// </summary>
    public int Top { get; }

    /// <summary>
    /// The denominator when writing time signature as a fraction
    /// </summary>
    public int Bottom { get; }

    private TimeSignature(int top, int bottom)
    {
        Top = top;
        Bottom = bottom;
    }

    public static TimeSignature FromCommonTime()
    {
        return new TimeSignature(4, 4);
    }

    public static TimeSignature FromCutTime()
    {
        return new TimeSignature(2, 2);
    }

    public static TimeSignature FromFractionalTime(Ratio fraction)
    {
        return new TimeSignature(fraction.Numerator, fraction.Denominator);
    }
}