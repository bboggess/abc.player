﻿namespace abc.parser.model;

/// <summary>
/// Metadata for a tune, from fields defined in the ABC header spec.
/// </summary>
public class TuneHeader
{
    private TuneHeader(
        TrackIndex index,
        string title,
        Composer composer,
        KeySignature key,
        Ratio baseNoteLength,
        TimeSignature meter,
        TempoDefinition tempo
    )
    {
        Index = index;
        Title = title;
        Composer = composer;
        Key = key;
        BaseNoteLength = baseNoteLength;
        Meter = meter;
        Tempo = tempo;
    }

    /// <summary>
    /// Unique identifier for a track to distinguish within collections.
    /// </summary>
    public TrackIndex Index { get; }

    /// <summary>
    /// The name of the tune. Guaranteed not null or empty.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// The person or group who composed this piece.
    /// </summary>
    public Composer Composer { get; }

    /// <summary>
    /// The key signature for the piece as a whole. We do not currently
    /// support changing key mid-tune.
    /// </summary>
    public KeySignature Key { get; }

    /// <summary>
    /// The note length to be used when no length is specified. All other
    /// note lengths are described as multiples of this.
    /// </summary>
    public Ratio BaseNoteLength { get; }

    /// <summary>
    /// The time signature for the entire piece. We do not currently
    /// allow pieces to change meter.
    /// </summary>
    public TimeSignature Meter { get; }

    /// <summary>
    /// The tempo the tune should be played at.
    /// </summary>
    public TempoDefinition Tempo { get; }

    /// <summary>
    /// Providers a builder that can be used to construct a header one field at a time.
    /// </summary>
    /// <param name="defaults">Provides default values for optional fields.</param>
    /// <returns>A builder that will use given defaults for unspecified, optional fields.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="defaults"/> is null</exception>
    public static ITuneHeaderBuilder Builder(IFieldDefaults defaults)
    {
        if (defaults is null)
        {
            throw new ArgumentNullException(nameof(defaults));
        }

        return new FieldBuilder(defaults);
    }

    /// <summary>
    /// Build a <see cref="TuneHeader"/>, supplying fields one at a time.
    /// Substitutes in default values for missing optional fields. If you don't give a required
    /// field, building will throw a <see cref="RequiredFieldException"/>.
    /// </summary>
    private class FieldBuilder : ITuneHeaderBuilder
    {
        private readonly IFieldDefaults _defaults;

        private TrackIndex? _index;
        private string? _title;
        private KeySignature? _key;
        private Composer? _composer;
        private Ratio? _baseNoteLength;
        private TimeSignature? _meter;
        private TempoDefinition? _tempo;

        internal FieldBuilder(IFieldDefaults defaults)
        {
            if (defaults is null)
            {
                throw new ArgumentNullException(nameof(defaults));
            }

            _defaults = defaults;
        }

        private FieldBuilder(FieldBuilder other)
        {
            _defaults = other._defaults;
            _index = other._index;
            _title = other._title;
            _key = other._key;
            _composer = other._composer;
            _baseNoteLength = other._baseNoteLength;
            _meter = other._meter;
            _tempo = other._tempo;
        }

        private TrackIndex Index => _index ?? throw new RequiredFieldException(nameof(Index));

        private string Title => _title ?? throw new RequiredFieldException(nameof(Title));

        private KeySignature KeySignature =>
            _key ?? throw new RequiredFieldException(nameof(KeySignature));

        private Composer Composer => _composer ?? _defaults.DefaultComposer;

        private Ratio BaseNoteLength => _baseNoteLength ?? _defaults.DefaultBaseNoteLength;

        private TimeSignature Meter => _meter ?? _defaults.DefaultMeter;

        private TempoDefinition Tempo => _tempo ?? _defaults.DefaultTempo;

        public ITuneHeaderBuilder WithTrackIndex(TrackIndex index)
        {
            return new FieldBuilder(this) { _index = index };
        }

        public ITuneHeaderBuilder WithTitle(string title)
        {
            return new FieldBuilder(this) { _title = title };
        }

        public ITuneHeaderBuilder WithKey(KeySignature key)
        {
            return new FieldBuilder(this) { _key = key };
        }

        public ITuneHeaderBuilder WithComposer(Composer composer)
        {
            return new FieldBuilder(this) { _composer = composer };
        }

        public ITuneHeaderBuilder WithDefaultNoteLength(Ratio noteLength)
        {
            return new FieldBuilder(this) { _baseNoteLength = noteLength };
        }

        public ITuneHeaderBuilder WithMeter(TimeSignature meter)
        {
            return new FieldBuilder(this) { _meter = meter };
        }

        public ITuneHeaderBuilder WithTempo(TempoDefinition tempo)
        {
            return new FieldBuilder(this) { _tempo = tempo };
        }

        public TuneHeader Build()
        {
            return new TuneHeader(
                Index,
                Title,
                Composer,
                KeySignature,
                BaseNoteLength,
                Meter,
                Tempo
            );
        }

        public class RequiredFieldException : Exception
        {
            public RequiredFieldException(string name)
                : base($"Missing required field: {name}") { }
        }
    }
}
