using abc.parser.model;
using abc.parser.PitchAdjustment;

namespace abc.parser.test;

public class AccidentalPitchAdjusterTests
{
    [Test]
    public void AdjustsMatchingPitch()
    {
        var startingPitch = new Pitch(BaseNote.C, 0);
        var adjuster = new AccidentalPitchAdjuster(BaseNote.C, 1);

        var resultPitch = adjuster.AdjustPitch(startingPitch);

        Assert.That(resultPitch.MidiValue, Is.EqualTo(startingPitch.MidiValue + 1));
    }

    [Test]
    public void DoesNotAdjustOtherPitches()
    {
        var startingPitch = new Pitch(BaseNote.C, 0);
        var adjuster = new AccidentalPitchAdjuster(BaseNote.D, 1);

        var resultPitch = adjuster.AdjustPitch(startingPitch);

        Assert.That(resultPitch.MidiValue, Is.EqualTo(startingPitch.MidiValue));
    }
}
