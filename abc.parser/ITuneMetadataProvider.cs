using abc.parser.model;

namespace abc.parser;

/// <summary>
/// Provides all metadata fields configured in the header of an ABC file.
/// </summary>
public interface ITuneHeaderProvider
{
    TuneHeader GetTuneHeader();
}
