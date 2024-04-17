namespace abc.parser.parse;

/// <summary>
/// As the tune body is parsed, relevant events will be raised with context from the tune.
/// This interface specifies the handlers that are needed to parse an ABC file.
/// </summary>
public interface ITuneBodyParser
{
    void AddNote(BodyNoteEvent e);
    void AddRest(BodyRestEvent e);
    void AddAccidental(BodyAccidentalEvent e);
}
