using abc.parser.model;

namespace abc.parser;

/// <summary>
/// Provides default values for optional ABC header fields.
/// </summary>
public interface IFieldDefaults
{
    Composer DefaultComposer { get; }
    TimeSignature DefaultMeter { get; }
    Ratio DefaultBaseNoteLength { get; }
    TempoDefinition DefaultTempo { get; }
}
