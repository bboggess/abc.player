using abc.parser.model;

namespace abc.parser.key;

/// <summary>
/// Steps around the circle of fifths in custom ways. Doesn't include
/// any termination logic, consumers will decide when to stop.
/// </summary>
internal interface ICircleStepper
{
    /// <summary>
    /// This indicates where in the circle we will start from.
    /// </summary>
    BaseNote GetStart();

    /// <summary>
    /// Gets the next note to visit.
    /// </summary>
    /// <param name="current">This is the note we are stepping away from.</param>
    BaseNote Next(BaseNote current);
}
