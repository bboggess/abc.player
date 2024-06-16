namespace abc.parser.parse;

/// <summary>
/// Describes the length of a note or rest in the ABC format. That is, there are two
/// optional multipliers applied to the base note length -- one for the numerator
/// and one for the denominator.
/// </summary>
public class NoteDuration
{
    public int? Numerator { get; set; }
    public int? Denominator { get; set; }

    /// <summary>
    /// Specifies that the default denominator was specified, rather than
    /// no denominator at all.
    /// </summary>
    public bool IsSpecifiedDenominator { get; set; }
}
