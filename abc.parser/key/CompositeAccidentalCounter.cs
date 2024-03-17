using abc.parser.model;

namespace abc.parser.key;

/// <summary>
/// A counter that will try multiple counters, using the first count
/// that succeeded in matching a key.
/// </summary>
internal class CompositeAccidentalCounter : IAccidentalCounter
{
    private readonly IEnumerable<IAccidentalCounter> _counters;

    /// <summary>
    /// Wraps multiple counters in a first-match counter.
    /// </summary>
    /// <param name="counters">
    /// The counters to use. We will use the first counter to return a valid value, so these need to be in order of preference
    /// </param>
    /// <exception cref="ArgumentNullException"><paramref name="counters"/> is null</exception>
    public CompositeAccidentalCounter(IEnumerable<IAccidentalCounter> counters)
    {
        if (counters is null)
        {
            throw new ArgumentNullException(nameof(counters));
        }

        _counters = counters;
    }

    public Optional<AccidentalCount> CountAccidentals(BaseNote tonic)
    {
        return _counters
            .Select(counter => counter.CountAccidentals(tonic))
            .FirstOrDefault(result => result.HasData, Optional<AccidentalCount>.None());
    }
}
