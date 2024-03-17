using abc.parser.model;

namespace abc.parser.key;

/// <summary>
/// Steps around the unit circle looking for minor keys using flats.
/// </summary>
internal class MinorFlatStepper : ICircleStepper
{
    private readonly CircleIntervalStepper _stepper;

    public MinorFlatStepper()
    {
        _stepper = CircleIntervalStepper.PerfectFourthStepper();
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
