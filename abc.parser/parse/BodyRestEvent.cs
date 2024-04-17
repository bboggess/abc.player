namespace abc.parser.parse;

/// <summary>
/// Event raised when a rest is encountered in the tune.
/// </summary>
public class BodyRestEvent
{
    /// <summary>
    /// The length of the rest, as a multiplier of the base note length.
    /// </summary>
    public NoteDuration Length { get; }

    public BodyRestEvent(NoteDuration length)
    {
        Length = length;
    }
}
