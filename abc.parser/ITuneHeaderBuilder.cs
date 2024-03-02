using abc.parser.model;

namespace abc.parser;

/// <summary>
/// A builder that can be used to construct a header one field at a time. Once
/// you have set all available fields, call <see cref="Build"/> to get a <see cref="TuneHeader"/>.
///
/// Note that the ABC spec requires all ABC files to have an index, title, and key set.
/// If you don't provide those required fields, implementations are allowed to throw exceptions.
/// </summary>
public interface ITuneHeaderBuilder
{
    TuneHeader Build();
    ITuneHeaderBuilder WithComposer(Composer composer);
    ITuneHeaderBuilder WithDefaultNoteLength(Ratio noteLength);
    ITuneHeaderBuilder WithTrackIndex(TrackIndex index);
    ITuneHeaderBuilder WithKey(KeySignature key);
    ITuneHeaderBuilder WithMeter(TimeSignature meter);
    ITuneHeaderBuilder WithTempo(TempoDefinition tempo);
    ITuneHeaderBuilder WithTitle(string title);
}
