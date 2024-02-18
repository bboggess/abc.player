namespace abc.parser.antlr.test.header;
public class MeterVisitorTests
{
    [Test]
    public void HandlesCommonTime()
    {
        var stringUnderTest = "C";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new MeterVisitor();

        var outcome = visitor.Visit(parser.timeSignature());

        Assert.Multiple(() =>
        {
            Assert.That(outcome.Top, Is.EqualTo(4));
            Assert.That(outcome.Bottom, Is.EqualTo(4));
        });
    }

    [Test]
    public void HandlesCutTime()
    {
        var stringUnderTest = "C|";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new MeterVisitor();

        var outcome = visitor.Visit(parser.timeSignature());

        Assert.Multiple(() =>
        {
            Assert.That(outcome.Top, Is.EqualTo(2));
            Assert.That(outcome.Bottom, Is.EqualTo(2));
        });
    }

    [Test]
    public void HandlesFractionalTime([Random(1, 32, 5)] int top, [Random(1, 32, 5)] int bottom)
    {
        var stringUnderTest = $"{top}/{bottom}";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new MeterVisitor();

        var outcome = visitor.Visit(parser.timeSignature());

        Assert.Multiple(() =>
        {
            Assert.That(outcome.Top, Is.EqualTo(top));
            Assert.That(outcome.Bottom, Is.EqualTo(bottom));
        });
    }

    [Test]
    public void HandleBadData()
    {
        var stringUnderTest = "D"; // arbitrary invalid meter specification
        var errorDetector = new ParserErrorDetector();
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest, errorDetector);
        var visitor = new MeterVisitor();

        var outcome = visitor.Visit(parser.timeSignature());

        Assert.Multiple(() =>
        {
            Assert.That(errorDetector.HasErrors, Is.True);
            Assert.That(outcome, Is.Null);
        });
    }
}
