using abc.parser.model;

namespace abc.parser.key;

/// <summary>
/// Steps around the circle of fifths with a constant interval, but no specified
/// starting note.
/// </summary>
internal class CircleIntervalStepper
{
    private const int _maxIntervalSize = 11;
    private const int _perfectFourthHalfSteps = 5;
    private const int _perfectFifthHalfSteps = 7;

    private readonly int _interval;

    private CircleIntervalStepper(int interval)
    {
        if (interval <= 0 || interval > _maxIntervalSize)
        {
            throw new ArgumentOutOfRangeException(nameof(interval));
        }

        _interval = interval;
    }

    public BaseNote Next(BaseNote cur)
    {
        return cur.Transpose(_interval);
    }

    public static CircleIntervalStepper PerfectFifthStepper()
    {
        return new CircleIntervalStepper(_perfectFifthHalfSteps);
    }

    public static CircleIntervalStepper PerfectFourthStepper()
    {
        return new CircleIntervalStepper(_perfectFourthHalfSteps);
    }
}
