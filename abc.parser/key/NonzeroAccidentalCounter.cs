using abc.parser.model;

namespace abc.parser.key;

/// <summary>
/// Intercepts an existing counter, enforcing the rule that a count of 0 accidentals is
/// not allowed. If the original counter returns 0, we change that to None.
/// </summary>
internal class NonzeroAccidentalCounter : IAccidentalCounter
{
    private readonly IAccidentalCounter _counter;

    public NonzeroAccidentalCounter(IAccidentalCounter counter)
    {
        if (counter is null)
        {
            throw new ArgumentNullException(nameof(counter));
        }

        _counter = counter;
    }

    public Optional<AccidentalCount> CountAccidentals(BaseNote tonic)
    {
        return _counter
            .CountAccidentals(tonic)
            .Bind(count =>
                count.NumAccidentals > 0
                    ? Optional<AccidentalCount>.Some(count)
                    : Optional<AccidentalCount>.None()
            );
    }
}
