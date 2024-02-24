using abc.parser.antlr.visitor;
using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.test.header;

public class MeterVisitorTests
{
    [Test]
    public void HandlesCommonTime()
    {
        var stringUnderTest = "M:C\n";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new MeterVisitor();

        var outcome = visitor.Visit(parser.fieldMeter());

        Assert.Multiple(() =>
        {
            Assert.That(outcome.Top, Is.EqualTo(4));
            Assert.That(outcome.Bottom, Is.EqualTo(4));
        });
    }

    [Test]
    public void HandlesCutTime()
    {
        var stringUnderTest = "M:C|\n";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new MeterVisitor();

        var outcome = visitor.Visit(parser.fieldMeter());

        Assert.Multiple(() =>
        {
            Assert.That(outcome.Top, Is.EqualTo(2));
            Assert.That(outcome.Bottom, Is.EqualTo(2));
        });
    }

    [Test]
    public void HandlesFractionalTime([Random(1, 32, 5)] int top, [Random(1, 32, 5)] int bottom)
    {
        var stringUnderTest = $"M:{top}/{bottom}\n";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new MeterVisitor();

        var outcome = visitor.Visit(parser.fieldMeter());

        Assert.Multiple(() =>
        {
            Assert.That(outcome.Top, Is.EqualTo(top));
            Assert.That(outcome.Bottom, Is.EqualTo(bottom));
        });
    }

    [Test]
    public void HandleBadData()
    {
        var stringUnderTest = "M:D\n"; // arbitrary invalid meter specification
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);

        var action = () =>
        {
            _ = parser.fieldMeter();
        };

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }
}
