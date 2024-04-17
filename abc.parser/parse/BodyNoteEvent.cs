namespace abc.parser.parse;

/// <summary>
/// Event raised when a note is found in the tune body.
/// </summary>
public class BodyNoteEvent
{
    /// <summary>
    /// The (naturalized) note to play. This does not include accidental info, that should
    /// be inferred from context.
    ///
    /// If uppercase, we're in the octave of middle C. If lowercase, we're one octave down.
    /// </summary>
    public char Pitch { get; }

    /// <summary>
    /// Moves the octave of <see cref="Pitch"/> up or down. If empty, no changes should be applied.
    ///
    /// See the ABC spec for how this should be formatted.
    /// </summary>
    public string OctaveDescriptor { get; }

    /// <summary>
    /// Description of the length of the note. Works as a multiplier on the base note length.
    /// </summary>
    public NoteDuration Length { get; }

    public BodyNoteEvent(char pitch, string octaveDescriptor, NoteDuration length)
    {
        Pitch = pitch;
        OctaveDescriptor = octaveDescriptor;
        Length = length;
    }
}
