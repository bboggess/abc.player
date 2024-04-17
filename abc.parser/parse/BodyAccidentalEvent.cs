namespace abc.parser.parse;

/// <summary>
/// Event raised when an accidental is encountered in the body of a tune.
/// </summary>
public class BodyAccidentalEvent
{
    /// <summary>
    /// The (natural) note that the accidental should be applied to.
    /// </summary>
    public char Pitch { get; }

    /// <summary>
    /// ABC description of the accidental to apply. This can be
    /// double flat, flat, natural, sharp, or double sharp.
    ///
    /// See the ABC spec for the format.
    /// </summary>
    public string AccidentalDescriptor { get; }

    public BodyAccidentalEvent(char pitch, string accidentalDescriptor)
    {
        Pitch = pitch;
        AccidentalDescriptor = accidentalDescriptor;
    }
}
