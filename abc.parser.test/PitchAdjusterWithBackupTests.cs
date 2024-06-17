using abc.parser.model;
using abc.parser.PitchAdjustment;
using Moq;

namespace abc.parser.test;

public class PitchAdjusterWithBackupTests
{
    [Test]
    public void IfTargeting_CallsPreferredAdjuster()
    {
        var mockPreferred = new Mock<ITargetedPitchAdjuster>();
        var mockBackup = new Mock<IPitchAdjuster>();
        mockPreferred.Setup(adjuster => adjuster.IsTargetingPitch(It.IsAny<Pitch>())).Returns(true);

        var adjuster = new PitchAdjusterWithBackup(mockPreferred.Object, mockBackup.Object);
        var testPitch = new Pitch(BaseNote.C, 0);

        adjuster.AdjustPitch(testPitch);

        mockPreferred.Verify(m => m.AdjustPitch(It.Is<Pitch>(p => p == testPitch)), Times.Once);
    }

    [Test]
    public void IfTargeting_DoesNotCallBackupAdjuster()
    {
        var mockPreferred = new Mock<ITargetedPitchAdjuster>();
        var mockBackup = new Mock<IPitchAdjuster>();
        mockPreferred.Setup(adjuster => adjuster.IsTargetingPitch(It.IsAny<Pitch>())).Returns(true);

        var adjuster = new PitchAdjusterWithBackup(mockPreferred.Object, mockBackup.Object);
        var testPitch = new Pitch(BaseNote.C, 0);

        adjuster.AdjustPitch(testPitch);

        mockBackup.Verify(m => m.AdjustPitch(It.IsAny<Pitch>()), Times.Never);
    }

    [Test]
    public void IfNotTargeting_DoesNotCallPreferredAdjuster()
    {
        var mockPreferred = new Mock<ITargetedPitchAdjuster>();
        var mockBackup = new Mock<IPitchAdjuster>();
        mockPreferred
            .Setup(adjuster => adjuster.IsTargetingPitch(It.IsAny<Pitch>()))
            .Returns(false);

        var adjuster = new PitchAdjusterWithBackup(mockPreferred.Object, mockBackup.Object);
        var testPitch = new Pitch(BaseNote.C, 0);

        adjuster.AdjustPitch(testPitch);

        mockPreferred.Verify(m => m.AdjustPitch(It.IsAny<Pitch>()), Times.Never);
    }

    [Test]
    public void IfNotTargeting_CallsBackupAdjuster()
    {
        var mockPreferred = new Mock<ITargetedPitchAdjuster>();
        var mockBackup = new Mock<IPitchAdjuster>();
        mockPreferred
            .Setup(adjuster => adjuster.IsTargetingPitch(It.IsAny<Pitch>()))
            .Returns(false);

        var adjuster = new PitchAdjusterWithBackup(mockPreferred.Object, mockBackup.Object);
        var testPitch = new Pitch(BaseNote.C, 0);

        adjuster.AdjustPitch(testPitch);

        mockBackup.Verify(m => m.AdjustPitch(It.Is<Pitch>(p => p == testPitch)), Times.Once);
    }
}
