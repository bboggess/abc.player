using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using NUnit.Framework;

namespace abc.parser.antlr.test.header;

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

        var parser = SetupHeaderHelpers.SetUpParser(tempoHeader);
        var parseTree = parser.fieldTempo();
        var walker = new ParseTreeWalker();

        walker.Walk(fakeListener, parseTree);

        var expectedTempo = $"{numer}/{denom}={bpm}";
        Assert.That(fakeListener.TempoDesc, Is.EqualTo(expectedTempo));
    }

    [Test]
    public void ParserErrorIfNoBeatSpecified([Random(5)] int bpm)
    {
        var missingBeat = $"Q:{bpm}\n";
        var parser = SetupHeaderHelpers.SetUpParser(missingBeat);

        var action = () => { _ = parser.fieldTempo(); };

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }

    [Test]
    public void ParserErrorIfNoFraction([Random(3)] int beat, [Random(3)] int bpm)
    {
        var missingBeat = $"Q:{beat}={bpm}\n";
        var parser = SetupHeaderHelpers.SetUpParser(missingBeat);

        var action = () => { _ = parser.fieldTempo(); };

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }

[Test]
    public void ParserErrorIfNoBpm([Random(3)] int numer, [Random(3)] int denom)
    {
        var missingBeat = $"Q:{numer}/{denom}\n";
        var parser = SetupHeaderHelpers.SetUpParser(missingBeat);

        var action = () => { _ = parser.fieldTempo(); };

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }
}
