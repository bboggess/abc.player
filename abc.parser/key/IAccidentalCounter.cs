using abc.parser.model;

namespace abc.parser.key;

/// <summary>
/// Counts the number of accidentals in a key, and can report whether
/// those are flats or sharps.
/// </summary>
internal interface IAccidentalCounter
{
    /// <summary>
    /// From the given tonic, counts the number of accidentals that should be used.
    /// It may be that this counter can't find an appropriate key to use.
    /// </summary>
    /// <param name="tonic">Count accidentals for this tonic.</param>
    /// <returns>
    /// Either the nonnegative count of the number of accidentals to use, or an empty Optional
    /// if no valid value could be found.
    /// </returns>
    Optional<AccidentalCount> CountAccidentals(BaseNote tonic);
}
