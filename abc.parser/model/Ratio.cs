namespace abc.parser.model;

/// <summary>
/// A ratio between two nonnegative values. This is meant to be used to
/// represent musical ratios, and so we do not simplify ratios. In other words,
/// 4/4 and 2/2 should be distinct.
/// </summary>
public class Ratio
{
    public int Numerator { get; internal set; }
    public int Denominator { get; internal set; }

    /// <summary>
    /// Constructs a ratio of two nonnegative numbers.
    /// </summary>
    /// <param name="numerator">When thinking of the ratio as a fraction, the top</param>
    /// <param name="denominator">When thinking of the ratio as a fraction, the bottom</param>
    /// <exception cref="ArgumentException">
    /// Either of <paramref name="denominator"/> or <paramref name="numerator"/> are negative or <paramref name="denominator"/> is 0.
    /// </exception>
    public Ratio(int numerator, int denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("Denominator cannot be 0", nameof(denominator));
        }

        if (numerator < 0)
        {
            throw new ArgumentException("Ratios must use positive values", nameof(numerator));
        }

        if (denominator < 0)
        {
            throw new ArgumentException("Ratios must use positive values", nameof(denominator));
        }

        Numerator = numerator;
        Denominator = denominator;
    }

    /// <summary>
    /// Constructs a ratio as a ratio of two ratios.
    /// </summary>
    /// <param name="top">The numerator</param>
    /// <param name="bottom">The denominator</param>
    public Ratio(Ratio top, Ratio bottom)
        : this(top.Numerator * bottom.Denominator, top.Denominator * bottom.Numerator) { }

    /// <summary>
    /// Returns the ratio as a decimal, when you don't need an exact representation.
    /// </summary>
    /// <returns>decimal approximating the ratio</returns>
    public double ToDecimal()
    {
        return Numerator / Denominator;
    }

    public override bool Equals(object? obj)
    {
        var other = obj as Ratio;

        // Remember we do not simplify, so this is really just a straight equality check
        return other is not null
            && Numerator == other.Numerator
            && Denominator == other.Denominator;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Numerator, Denominator);
    }
}
