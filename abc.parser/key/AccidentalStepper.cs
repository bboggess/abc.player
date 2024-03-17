using abc.parser.model;

namespace abc.parser.key;

/// <summary>
/// Steps around the circle of fifths in the appropriate order for
/// adding accidentals. For example, sharps are always added in the order
/// F, C, G, etc.
/// </summary>
internal abstract class AccidentalStepper : ICircleStepper
{
    private readonly CircleIntervalStepper _stepper;

    protected AccidentalStepper(CircleIntervalStepper stepper)
    {
        _stepper = stepper;
    }

    public abstract BaseNote GetStart();

    public BaseNote Next(BaseNote current)
    {
        return _stepper.Next(current);
    }

    /// <summary>
    /// Returns the set of all notes that should be given accidentals.
    /// </summary>
    /// <param name="numAccidentals">The number of accidentals in the key.</param>
    /// <returns>a possibly empty set of notes that should have accidentals applied</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="numAccidentals"/> is negative</exception>
    public ISet<BaseNote> GetAccidentals(int numAccidentals)
    {
        if (numAccidentals < 0 || numAccidentals > KeySignature.MaxAccidentals)
        {
            throw new ArgumentOutOfRangeException(nameof(numAccidentals));
        }

        var accidentals = new HashSet<BaseNote>(numAccidentals);

        var cur = GetStart();
        for (int i = 0; i < numAccidentals; i++)
        {
            accidentals.Add(cur);
            cur = Next(cur);
        }

        return accidentals;
    }

    /// <summary>
    /// Constructs an <see cref="AccidentalStepper"/> based on whether we're using
    /// sharps or flats.
    /// </summary>
    /// <param name="type">Specifies either sharps or flats</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="type"/> is invalid enum value</exception>
    public static AccidentalStepper FromAccidentalType(AccidentalType type)
    {
        return type switch
        {
            AccidentalType.Sharp => new SharpAccidentalStepper(),
            AccidentalType.Flat => new FlatAccidentalStepper(),
            _ => throw new ArgumentOutOfRangeException(nameof(type)),
        };
    }
}

/// <summary>
/// Steps around the circle of fifths counting flats
/// </summary>
internal sealed class FlatAccidentalStepper : AccidentalStepper
{
    public FlatAccidentalStepper()
        : base(CircleIntervalStepper.PerfectFourthStepper()) { }

    public override BaseNote GetStart()
    {
        return BaseNote.B;
    }
}

/// <summary>
/// Steps around the circle of fifths counting sharps
/// </summary>
internal sealed class SharpAccidentalStepper : AccidentalStepper
{
    public SharpAccidentalStepper()
        : base(CircleIntervalStepper.PerfectFifthStepper()) { }

    public override BaseNote GetStart()
    {
        return BaseNote.F;
    }
}
