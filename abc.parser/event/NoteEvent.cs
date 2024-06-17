using abc.parser.model;

namespace abc.parser.@event;

/// <summary>
/// Raised when a new note has been encountered in parsing the tune.
/// </summary>
public class NoteEvent
{
    internal NoteEvent(Pitch note, Ratio length)
    {
        Note = note;
        Length = length;
    }

    /// <summary>
    /// The pitch being played.
    /// </summary>
    public Pitch Note { get; }

    /// <summary>
    /// The duration the note is held. 1/4 means a quarter note.
    /// </summary>
    public Ratio Length { get; }
}
