using Antlr4.Runtime.Tree;

namespace abc.parser.antlr.test;

/// <summary>
/// Tests for parsing the tempo map.
/// </summary>
public class TempoHeaderTests
{
    [Test]
    public void AcceptsValidFormat([Random(1, 32, 3)] int numer, [Random(1, 32, 3)] int denom, [Random(3)] int bpm)
    {
        var tempoHeader = $"Q:{numer}/{denom}={bpm}\n";
        var fakeListener = new TestHeaderListener();
        var errorDetector = new ParserErrorDetector();

        var parser = SetupHelpers.SetUpParser(tempoHeader, errorDetector);
        var parseTree = parser.fieldTempo();
        var walker = new ParseTreeWalker();

        walker.Walk(fakeListener, parseTree);

        var expectedTempo = $"{numer}/{denom}={bpm}";
        Assert.Multiple(() =>
        {
            Assert.That(fakeListener.TempoDesc, Is.EqualTo(expectedTempo));
            Assert.That(errorDetector.HasErrors, Is.False);
        });
    }

    [Test]
    public void ParserErrorIfNoBeatSpecified([Random(5)] int bpm)
    {
        var missingBeat = $"Q:{bpm}\n";
        var errorDetector = new ParserErrorDetector();
        var parser = SetupHelpers.SetUpParser(missingBeat, errorDetector);

        var _ = parser.fieldTempo();

        Assert.That(errorDetector.HasErrors, Is.True);
    }

    [Test]
    public void ParserErrorIfNoFraction([Random(3)] int beat, [Random(3)] int bpm)
    {
        var missingBeat = $"Q:{beat}={bpm}\n";
        var errorDetector = new ParserErrorDetector();
        var parser = SetupHelpers.SetUpParser(missingBeat, errorDetector);

        var _ = parser.fieldTempo();

        Assert.That(errorDetector.HasErrors, Is.True);
    }

    [Test]
    public void ParserErrorIfNoBpm([Random(3)] int numer, [Random(3)] int denom)
    {
        var missingBeat = $"Q:{numer}/{denom}\n";
        var errorDetector = new ParserErrorDetector();
        var parser = SetupHelpers.SetUpParser(missingBeat, errorDetector);

        var _ = parser.fieldTempo();

        Assert.That(errorDetector.HasErrors, Is.True);
    }
}
