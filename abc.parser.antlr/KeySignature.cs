namespace abc.parser.antlr;

public enum Mode
{
    Major,
    Minor,
}

/// <summary>
/// Simple model for a key signature parsed from an ABC file.
/// </summary>
public class KeySignature
{
    public Note Tonic { get; }
    public Mode Mode { get; }

    public KeySignature(Note tonic, Mode mode)
    {
        Tonic = tonic;
        Mode = mode;
    }
}