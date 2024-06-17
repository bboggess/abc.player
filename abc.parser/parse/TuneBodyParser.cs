using abc.parser.@event;
using abc.parser.model;
using abc.parser.PitchAdjustment;

namespace abc.parser.parse;

/// <summary>
/// Mediator for taking raw messages from the body parser and coordinating converting
/// those into rich domain models and firing off messages to domain handlers.
/// </summary>
public class TuneBodyParser : ITuneBodyParser
{
    private readonly ITuneEventListener<NoteEvent> _noteListener;
    private readonly ITuneEventListener<RestEvent> _restListener;
    private readonly IScoreContext _context;

    public TuneBodyParser(
        ITuneEventListener<NoteEvent> noteListener,
        ITuneEventListener<RestEvent> restListener,
        IScoreContext context
    )
    {
        _noteListener = noteListener;
        _restListener = restListener;
        _context = context;
    }

    public void AddAccidental(BodyAccidentalEvent e)
    {
        var accidental = ParseAccidental(e.Pitch, e.AccidentalDescriptor);
        _context.AddNewAccidental(accidental);
    }

    public void AddNote(BodyNoteEvent e)
    {
        var writtenDuration = ParseDuration(e.Length);
        var writtenPitch = ParsePitch(e.Pitch, e.OctaveDescriptor);

        var length = _context.ResolveDuration(writtenDuration);
        var pitch = _context.ResolvePitch(writtenPitch);

        var noteEvent = new NoteEvent(pitch, length);
        _noteListener.Handle(noteEvent);
    }

    public void AddRest(BodyRestEvent e)
    {
        var duration = ParseDuration(e.Length);

        var length = _context.ResolveDuration(duration);

        var restEvent = new RestEvent(length);
        _restListener.Handle(restEvent);
    }

    private static DurationDescription ParseDuration(NoteDuration length)
    {
        var builder = new DurationDescription.DurationBuilder();

        if (length.Numerator is int numerator)
        {
            builder.WithNumerator(numerator);
        }

        if (length.Denominator is int denominator)
        {
            builder.WithDenominator(denominator);
        }
        else if (length.IsSpecifiedDenominator)
        {
            builder.WithDefaultDenominator();
        }

        return builder.Build();
    }

    private static Pitch ParsePitch(char note, string octaveDescriptor)
    {
        var basePitch = Pitch.ParseFromNoteName(note);
        var octaveModifier = OctaveModifier.FromDescriptor(octaveDescriptor);
        return octaveModifier.AdjustPitch(basePitch);
    }

    private static BodyAccidental ParseAccidental(char pitch, string accidentalDescriptor)
    {
        return BodyAccidental.FromDescriptor(pitch, accidentalDescriptor);
    }
}
