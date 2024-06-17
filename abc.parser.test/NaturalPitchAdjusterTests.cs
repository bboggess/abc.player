using abc.parser.model;

namespace abc.parser.test;

public class NaturalPitchAdjusterTests
{
    [Test]
    public void IgnoresMatchingPitch()
    {
        var startingPitch = new Pitch(BaseNote.C, 0);
        var adjuster = new NaturalPitchAdjuster(BaseNote.C);

        var resultPitch = adjuster.AdjustPitch(startingPitch);

        Assert.That(resultPitch.MidiValue, Is.EqualTo(startingPitch.MidiValue));
    }

    [Test]
    public void DoesNotAdjustOtherPitches()
    {
        var startingPitch = new Pitch(BaseNote.C, 0);
        var adjuster = new NaturalPitchAdjuster(BaseNote.D);

        var resultPitch = adjuster.AdjustPitch(startingPitch);

        Assert.That(resultPitch.MidiValue, Is.EqualTo(startingPitch.MidiValue));
    }

    [Test]
    public void TargetsMatchingPitch()
    {
        var startingPitch = new Pitch(BaseNote.C, 0);
        var adjuster = new NaturalPitchAdjuster(BaseNote.C);

        var isTargeting = adjuster.IsTargetingPitch(startingPitch);

        Assert.That(isTargeting, Is.True);
    }

    [Test]
    public void DoesNotTargetOtherPitch()
    {
        var startingPitch = new Pitch(BaseNote.C, 0);
        var adjuster = new NaturalPitchAdjuster(BaseNote.D);

        var isTargeting = adjuster.IsTargetingPitch(startingPitch);

        Assert.That(isTargeting, Is.False);
    }
}
