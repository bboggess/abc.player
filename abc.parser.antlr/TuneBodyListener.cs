using abc.parser.parse;
using Antlr4.Runtime.Misc;

namespace abc.parser.antlr;

/// <summary>
/// Listener for a walk over an ABC tune body. Raises the appropriate events that the higher
/// level domain parsing logic can handle.
/// </summary>
public class TuneBodyListener : AbcBodyBaseListener
{
    private readonly ITuneBodyParser _parser;
    private NoteParseContext _currentNote;

    public TuneBodyListener(ITuneBodyParser parser)
    {
        if (parser is null)
        {
            throw new ArgumentNullException(nameof(parser));
        }

        _parser = parser;
        _currentNote = new NoteParseContext();
    }

    public override void ExitNote([NotNull] AbcBodyParser.NoteContext context)
    {
        if (_currentNote.Duration is not NoteDuration duration)
        {
            // If we somehow manage to not have a duration set here, something has gone terribly wrong.
            // It's required in the grammar.
            throw new ParseException(context);
        }

        if (_currentNote.Pitch is char pitch)
        {
            var noteEvent = new BodyNoteEvent(
                pitch,
                _currentNote.OctaveDescriptor ?? string.Empty,
                duration
            );

            _parser.AddNote(noteEvent);
        }
        else
        {
            var restEvent = new BodyRestEvent(duration);
            _parser.AddRest(restEvent);
        }

        _currentNote = new NoteParseContext();
    }

    public override void EnterOctave([NotNull] AbcBodyParser.OctaveContext context)
    {
        _currentNote.OctaveDescriptor = context.GetText();
    }

    public override void EnterAccidental([NotNull] AbcBodyParser.AccidentalContext context)
    {
        if (_currentNote.Pitch is not char pitch)
        {
            // With the grammar structure, we always expect a pitch to have been set before
            // encountering any accidentals.
            throw new ParseException(context);
        }

        var accidentalDescriptor = context.GetText();

        var accidentalEvent = new BodyAccidentalEvent(pitch, accidentalDescriptor);
        _parser.AddAccidental(accidentalEvent);
    }

    public override void EnterPitch([NotNull] AbcBodyParser.PitchContext context)
    {
        var writtenNote = context.baseNote().GetText();

        if (string.IsNullOrEmpty(writtenNote))
        {
            throw new ParseException(context);
        }

        _currentNote.Pitch = writtenNote.First();
    }

    public override void EnterNoteLength([NotNull] AbcBodyParser.NoteLengthContext context)
    {
        var pieces = context.GetText().Split('/');
        var includesSlash = pieces.Length > 1;

        var numeratorText = pieces.FirstOrDefault();
        var denominatorText = includesSlash ? pieces.LastOrDefault() : string.Empty;

        var duration = new NoteDuration { UseDefaultDenominator = includesSlash };

        if (!string.IsNullOrEmpty(numeratorText))
        {
            if (!int.TryParse(numeratorText, out int numerator))
            {
                throw new ParseException(context);
            }

            duration.Numerator = numerator;
        }

        if (!string.IsNullOrEmpty(denominatorText))
        {
            if (!int.TryParse(denominatorText, out int denominator))
            {
                throw new ParseException(context);
            }

            duration.Denominator = denominator;
        }

        _currentNote.Duration = duration;
    }

    private class NoteParseContext
    {
        public char? Pitch { get; set; }
        public NoteDuration? Duration { get; set; }
        public string? OctaveDescriptor { get; set; }
    }
}
