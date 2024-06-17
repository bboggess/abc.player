using abc.parser.model;

namespace abc.parser.@event;

/// <summary>
/// Event raised when a rest is encountered in the tune.
/// </summary>
public class RestEvent
{
    internal RestEvent(Ratio duration)
    {
        Duration = duration;
    }

    /// <summary>
    /// How long the rest should last. 1/4 means a quarter note rest.
    /// </summary>
    public Ratio Duration { get; }
}
