using abc.parser.parse;

namespace abc.parser;

/// <summary>
/// An object capable of walking through and processing a specific tune.
/// </summary>
public interface ITuneProcessor
{
    /// <summary>
    /// Processes the full tune body, using the provided parser
    /// to parse the raw data being processed.
    /// </summary>
    /// <param name="parser">Takes in unstructured events and parses them.</param>
    void Process(ITuneBodyParser parser);
}
