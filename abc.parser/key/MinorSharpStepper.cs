using abc.parser.model;

namespace abc.parser.key;

/// <summary>
/// Steps around the unit circle looking for minor keys using sharps.
/// </summary>
internal class MinorSharpStepper : ICircleStepper
{
    private readonly CircleIntervalStepper _stepper;

    public MinorSharpStepper()
    {
        _stepper = CircleIntervalStepper.PerfectFifthStepper();
    }

    public BaseNote GetStart()
    {
        return BaseNote.A;
    }

    public BaseNote Next(BaseNote current)
    {
        return _stepper.Next(current);
    }
}
