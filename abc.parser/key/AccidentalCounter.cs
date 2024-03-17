using abc.parser.model;

namespace abc.parser.key;

/// <summary>
/// A basic accidental counting strategy. Works by stepping around the circle of
/// fifths in a specified way. Will not use double accidentals, and so <see cref="CountAccidentals(BaseNote)"/>
/// can fail to return a value.
/// </summary>
internal class AccidentalCounter : IAccidentalCounter
{
    private readonly ICircleStepper _stepper;
    private readonly AccidentalType _accidental;

    /// <summary>
    /// Constructs an accidental counter that counts by stepping around the circle
    /// of fifths.
    /// </summary>
    /// <param name="stepper">Specifies how we will be looping around the circle of fifths</param>
    /// <param name="accidental">whether to count sharps or flats</param>
    /// <exception cref="ArgumentNullException"><paramref name="stepper"/> is null</exception>
    public AccidentalCounter(ICircleStepper stepper, AccidentalType accidental)
    {
        if (stepper is null)
        {
            throw new ArgumentNullException(nameof(stepper));
        }

        _stepper = stepper;
        _accidental = accidental;
    }

    public Optional<AccidentalCount> CountAccidentals(BaseNote tonic)
    {
        var cur = _stepper.GetStart();
        var numAccidentals = 0;

        // we don't support keys with double accidentals, so stop
        // if we hit that point
        while (numAccidentals < 8)
        {
            if (cur == tonic)
            {
                var count = new AccidentalCount(numAccidentals, _accidental);
                return Optional<AccidentalCount>.Some(count);
            }

            cur = _stepper.Next(cur);
            numAccidentals++;
        }

        return Optional<AccidentalCount>.None();
    }
}
