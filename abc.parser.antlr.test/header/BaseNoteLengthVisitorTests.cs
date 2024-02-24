using abc.parser.antlr.visitor;
using abc.parser.model;
using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.test.header;

public class BaseNoteLengthVisitorTests
{
    [Test]
    public void AcceptsValidRatios([Random(3)] int top, [Random(3)] int bottom)
    {
        var stringUnderTest = $"L:{top}/{bottom}\n";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new BaseNoteLengthVisitor();

        var outcome = visitor.Visit(parser.fieldLength());

        var expected = new Ratio(top, bottom);
        Assert.That(outcome, Is.EqualTo(expected));
    }

    [Test]
    public void RejectsMissingNumerator([Random(3)] int bottom)
    {
        var stringUnderTest = $"L:/{bottom}\n";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new BaseNoteLengthVisitor();

        var action = () => visitor.Visit(parser.fieldLength());

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }

    [Test]
    public void RejectsMissingDenominator([Random(3)] int top)
    {
        var stringUnderTest = $"L:{top}/\n";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new BaseNoteLengthVisitor();

        var action = () => visitor.Visit(parser.fieldLength());

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }

    [Test]
    public void RejectsNonFraction([Random(3)] int num)
    {
        var stringUnderTest = $"L:{num}\n";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new BaseNoteLengthVisitor();

        var action = () => visitor.Visit(parser.fieldLength());

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }
}
