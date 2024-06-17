namespace abc.parser.@event;

/// <summary>
/// A no-op listener, just eats the event.
/// </summary>
/// <typeparam name="TEvent">The event being responded to</typeparam>
public class NoneListener<TEvent> : ITuneEventListener<TEvent>
{
    public void Handle(TEvent e) { }
}
