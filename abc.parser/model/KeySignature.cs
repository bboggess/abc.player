namespace abc.parser.model;

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
    public KeyTonic Tonic { get; }
    public Mode Mode { get; }

    public KeySignature(KeyTonic tonic, Mode mode)
    {
        Tonic = tonic;
        Mode = mode;
    }
}
