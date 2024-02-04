using Antlr4.Runtime.Tree;

namespace abc.parser.antlr.test;

/// <summary>
/// Tests for logic specific to formatting the track's meter.
/// </summary>
public class MeterHeaderTests
{
    private const string COMMON_TIME_HEADER = """
                                                X:1
                                                T:Piece 1
                                                M:C
                                                K:C

                                                """;

    private const string CUT_TIME_HEADER = """
                                            X:1
                                            T:Piece 1
                                            M:C|
                                            K:C

                                            """;

    private const string INVALID_METER_HEADER = """
                                                X:1
                                                T:Piece 1
                                                M:D
                                                K:C

                                                """;

    private const string FRACTION_METER_HEADER = """
                                                X:1
                                                T:Piece 1
                                                M:31/16
                                                K:C

                                                """;

    [Test]
    public void AcceptsCommonTime()
    {
        var fakeListener = new TestHeaderListener();
        var parseTree = SetupHelpers.SetUpParseTree(COMMON_TIME_HEADER);
        var walker = new ParseTreeWalker();

        walker.Walk(fakeListener, parseTree);

        Assert.That(fakeListener.Meter, Is.EqualTo("C"));
    }

    [Test]
    public void AcceptsCutTime()
    {
        var fakeListener = new TestHeaderListener();
        var parseTree = SetupHelpers.SetUpParseTree(CUT_TIME_HEADER);
        var walker = new ParseTreeWalker();

        walker.Walk(fakeListener, parseTree);

        Assert.That(fakeListener.Meter, Is.EqualTo("C|"));
    }

    [Test]
    public void AcceptsFractionalMeter()
    {
        var fakeListener = new TestHeaderListener();
        var parseTree = SetupHelpers.SetUpParseTree(FRACTION_METER_HEADER);
        var walker = new ParseTreeWalker();

        walker.Walk(fakeListener, parseTree);

        Assert.That(fakeListener.Meter, Is.EqualTo("31/16"));
    }

    [Test]
    public void RejectsInvalidMeter()
    {
        var fakeListener = new TestHeaderListener();
        var parseTree = SetupHelpers.SetUpParseTree(INVALID_METER_HEADER);
        var walker = new ParseTreeWalker();

        walker.Walk(fakeListener, parseTree);

        Assert.That(fakeListener.Meter, Is.Empty);
    }

    [Test]
    public void ParserErrorOnInvalidMeter()
    {
        var errorDetector = new ParserErrorDetector();
        var parser = SetupHelpers.SetUpParser(INVALID_METER_HEADER, errorDetector);

        // Now let's actually parse the header
        var _ = parser.abcFile();

        Assert.That(errorDetector.HasErrors, Is.True);
    }
}
