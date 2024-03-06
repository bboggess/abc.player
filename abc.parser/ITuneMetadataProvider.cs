using abc.parser.model;

namespace abc.parser;

/// <summary>
/// Provides all metadata fields configured in the header of an ABC file.
/// </summary>
public interface ITuneHeaderProvider
{
    /// <summary>
    /// Grabs the information needed for describing a tune.
    /// </summary>
    /// <returns>A header domain model with all fields set.</returns>
    TuneHeader GetTuneHeader();
}
