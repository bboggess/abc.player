using System.ComponentModel;
using abc.parser.PitchAdjustment;

namespace abc.parser.model;

/// <summary>
/// An accidental marked in the score of a piece of music.
/// </summary>
public class BodyAccidental
{
    private const string DoubleFlat = "__";
    private const string Flat = "_";
    private const string Natural = "=";
    private const string Sharp = "^";
    private const string DoubleSharp = "^^";

    private enum Type
    {
        DoubleFlat,
        Flat,
        Natural,
        Sharp,
        DoubleSharp,
    }

    private readonly Type _type;
    private readonly BaseNote _note;

    private BodyAccidental(BaseNote note, Type type)
    {
        if (note is null)
        {
            throw new ArgumentNullException(nameof(note));
        }

        if (!Enum.IsDefined(type))
        {
            throw new InvalidEnumArgumentException(nameof(type));
        }

        _note = note;
        _type = type;
    }

    public static BodyAccidental FromDescriptor(char note, string descriptor)
    {
        var type = descriptor switch
        {
            DoubleFlat => Type.DoubleFlat,
            Flat => Type.Flat,
            Natural => Type.Natural,
            Sharp => Type.Sharp,
            DoubleSharp => Type.DoubleSharp,
            _
                => throw new ArgumentException(
                    $"Invalid accidental descriptor: {descriptor}",
                    nameof(descriptor)
                ),
        };

        return new BodyAccidental(BaseNote.FromChar(note), type);
    }

    public ITargetedPitchAdjuster ToPitchAdjuster()
    {
        return _type switch
        {
            Type.DoubleFlat => new AccidentalPitchAdjuster(_note, -2),
            Type.Flat => new AccidentalPitchAdjuster(_note, -1),
            Type.Natural => new NaturalPitchAdjuster(_note),
            Type.Sharp => new AccidentalPitchAdjuster(_note, 1),
            Type.DoubleSharp => new AccidentalPitchAdjuster(_note, 2),
            _
                => throw new InvalidEnumArgumentException(
                    $"Unrecognized enum for {nameof(_type)}: {_type}"
                ),
        };
    }
}
