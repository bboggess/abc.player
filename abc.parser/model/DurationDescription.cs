namespace abc.parser.model;

/// <summary>
/// Note duration as specified in the body of an ABC file. In particular, this
/// is really a duration relative to the default note length specified in the
/// header file. This should always be converted before use.
/// </summary>
public class DurationDescription
{
    private readonly int _numerator;
    private readonly int _denominator;

    private DurationDescription(int numerMultiplier, int denomMultiplier)
    {
        _numerator = numerMultiplier;
        _denominator = denomMultiplier;
    }

    /// <summary>
    /// The duration of a note is determined by multiplying the
    /// default note value by this multiplier.
    /// </summary>
    public Ratio Multiplier => new(_numerator, _denominator);

    public override bool Equals(object? obj)
    {
        if (obj is DurationDescription other)
        {
            return _numerator == other._numerator && _denominator == other._denominator;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_numerator, _denominator);
    }

    /// <summary>
    /// Builds a Duration from the various optional pieces used to notate note duration.
    /// </summary>
    internal class DurationBuilder
    {
        private const int DefaultNumerator = 1;
        private const int DefaultDenominator = 2;
        private const int NoDenominatorIncluded = 1;

        private int _numerator;
        private int _denominator;

        public DurationBuilder()
        {
            _numerator = DefaultNumerator;
            _denominator = NoDenominatorIncluded;
        }

        public void WithNumerator(int numerator)
        {
            if (numerator <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numerator), "Value must be positive");
            }

            _numerator = numerator;
        }

        public void WithDenominator(int denominator)
        {
            if (denominator <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(denominator),
                    "Value must be positive"
                );
            }

            _denominator = denominator;
        }

        public void WithDefaultDenominator()
        {
            _denominator = DefaultDenominator;
        }

        public DurationDescription Build()
        {
            return new DurationDescription(_numerator, _denominator);
        }
    }
}
