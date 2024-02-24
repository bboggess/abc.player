namespace abc.parser.model;

/// <summary>
/// A track index is a unique indentifier in a collection of tracks.
/// </summary>
public class TrackIndex
{
    /// <summary>
    /// An identifier for a track. Guaranteed nonnegative.
    /// </summary>
    public int Id { get; }

    public TrackIndex(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Track index cannot be negative");
        }

        Id = id;
    }
}
