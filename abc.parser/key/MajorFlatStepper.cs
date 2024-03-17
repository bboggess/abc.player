using abc.parser.model;

namespace abc.parser.key;

/// <summary>
/// Steps around the unit circle looking for major keys using flats.
/// </summary>
internal class MajorFlatStepper : ICircleStepper
{
    private readonly CircleIntervalStepper _stepper;

    public MajorFlatStepper()
    {
        _stepper = CircleIntervalStepper.PerfectFourthStepper();
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
