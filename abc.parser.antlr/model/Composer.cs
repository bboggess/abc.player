namespace abc.parser.antlr.model;

/// <summary>
/// Represents the composer of a piece of music.
/// </summary>
public class Composer
{
    /// <summary>
    /// The composer's name, guaranteed nonempty.
    /// </summary>
    public string Name { get; }

    public Composer(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        Name = name;
    }
}
