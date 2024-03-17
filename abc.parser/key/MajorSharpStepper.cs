using abc.parser.model;

namespace abc.parser.key;

/// <summary>
/// Steps around the unit circle looking for major keys using sharps.
/// </summary>
internal class MajorSharpStepper : ICircleStepper
{
    private readonly CircleIntervalStepper _stepper;

    public MajorSharpStepper()
    {
        _stepper = CircleIntervalStepper.PerfectFifthStepper();
    }

    public BaseNote GetStart()
    {
        return BaseNote.C;
    }

    public BaseNote Next(BaseNote current)
    {
        return _stepper.Next(current);
    }
}
