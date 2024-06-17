namespace abc.parser.@event;

/// <summary>
/// Generic interface for responding to parse events emitted from the parser.
/// </summary>
/// <typeparam name="TEvent">The message being handled</typeparam>
public interface ITuneEventListener<TEvent>
{
    void Handle(TEvent e);
}
