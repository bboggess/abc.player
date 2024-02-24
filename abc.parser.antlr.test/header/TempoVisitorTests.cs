using abc.parser.antlr.visitor;
using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.test.header;
public class TempoVisitorTests
{
    [Test]
    public void QuarterNoteBpm()
    {
        var stringUnderTest = $"1/4=120";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new TempoVisitor();

        var tempoDef = visitor.Visit(parser.tempoDef());

        Assert.That(tempoDef.QuarterNotePerMinute, Is.EqualTo(120));
    }

    [Test]
    public void HalfNoteBpm()
    {
        var stringUnderTest = $"1/2=60";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new TempoVisitor();

        var tempoDef = visitor.Visit(parser.tempoDef());

        Assert.That(tempoDef.QuarterNotePerMinute, Is.EqualTo(120));
    }

    [Test]
    public void ParserErrorIfNoBeatSpecified([Random(5)] int bpm)
    {
        var missingBeat = $"{bpm}";
        var parser = SetupHeaderHelpers.SetUpParser(missingBeat);
        var visitor = new TempoVisitor();

        var action = () => visitor.Visit(parser.tempoDef());

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }

    [Test]
    public void ParserErrorIfNoFraction([Random(3)] int beat, [Random(3)] int bpm)
    {
        var malformedPulse = $"{beat}={bpm}";
        var parser = SetupHeaderHelpers.SetUpParser(malformedPulse);
        var visitor = new TempoVisitor();

        var action = () => visitor.Visit(parser.tempoDef());

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }

    [Test]
    public void ParserErrorIfNoBpm([Random(3)] int numer, [Random(3)] int denom)
    {
        var noBpm = $"{numer}/{denom}";
        var parser = SetupHeaderHelpers.SetUpParser(noBpm);
        var visitor = new TempoVisitor();

        var action = () => visitor.Visit(parser.tempoDef());

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }
}
