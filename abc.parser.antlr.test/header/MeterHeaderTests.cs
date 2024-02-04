using Antlr4.Runtime.Tree;

namespace abc.parser.antlr.test.header;

/// <summary>
/// Tests for logic specific to formatting the track's meter.
/// </summary>
public class MeterHeaderTests
{
    [Test]
    public void AcceptsSpecialValues([Values("C", "C|")] string time)
    {
        var specialMeterHeader = $"M:{time}\n";
        var fakeListener = new TestHeaderListener();
        var errorDetector = new ParserErrorDetector();

        var parser = SetupHelpers.SetUpParser(specialMeterHeader, errorDetector);
        var parseTree = parser.fieldMeter();
        var walker = new ParseTreeWalker();

        walker.Walk(fakeListener, parseTree);

        Assert.Multiple(() =>
        {
            Assert.That(fakeListener.Meter, Is.EqualTo(time));
            Assert.That(errorDetector.HasErrors, Is.False);
        });
    }

    [Test]
    public void AcceptsFractionalMeter([Random(1, 32, 10)] int numer, [Random(1, 32, 10)] int denom)
    {
        var fractionalMeterHeader = $"M:{numer}/{denom}\n";
        var fakeListener = new TestHeaderListener();
        var errorDetector = new ParserErrorDetector();

        var parser = SetupHelpers.SetUpParser(fractionalMeterHeader, errorDetector);
        var parseTree = parser.fieldMeter();
        var walker = new ParseTreeWalker();

        walker.Walk(fakeListener, parseTree);

        var expectedMeter = $"{numer}/{denom}";
        Assert.Multiple(() =>
        {
            Assert.That(fakeListener.Meter, Is.EqualTo(expectedMeter));
            Assert.That(errorDetector.HasErrors, Is.False);
        });
    }

    [Test]
    public void ParserErrorOnInvalidMeter()
    {
        var invalidMeter = "M:D\n";
        var errorDetector = new ParserErrorDetector();
        var parser = SetupHelpers.SetUpParser(invalidMeter, errorDetector);

        var _ = parser.fieldMeter();

        Assert.That(errorDetector.HasErrors, Is.True);
    }
}
