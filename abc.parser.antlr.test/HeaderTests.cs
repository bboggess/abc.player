using Antlr4.Runtime.Tree;

namespace abc.parser.antlr.test;

/// <summary>
/// Runs test cases on a simple header with all supported fields.
/// 
/// Only asserts against the header, with no support for the tune body.
/// </summary>
public class HeaderTests
{
    private const string TEST_HEADER = """
                                        X:1
                                        T:Piece 1
                                        M:4/4 
                                        L:1/4
                                        C:W. Mozart
                                        Q:1/4=140
                                        K:C

                                        """;

    [Test]
    public void ParsesTitle()
    {
        var fakeListener = new TestHeaderListener();
        var parseTree = SetupHelpers.SetUpParseTree(TEST_HEADER);
        var walker = new ParseTreeWalker();

        walker.Walk(fakeListener, parseTree);

        Assert.That(fakeListener.Title, Is.EqualTo("Piece 1"));
    }

    [Test]
    public void ParsesTrackNum()
    {
        var fakeListener = new TestHeaderListener();
        var parseTree = SetupHelpers.SetUpParseTree(TEST_HEADER);
        var walker = new ParseTreeWalker();

        walker.Walk(fakeListener, parseTree);

        Assert.That(fakeListener.TrackNum, Is.EqualTo(1));
    }

    [Test]
    public void ParsesMeter()
    {
        var fakeListener = new TestHeaderListener();
        var parseTree = SetupHelpers.SetUpParseTree(TEST_HEADER);
        var walker = new ParseTreeWalker();

        walker.Walk(fakeListener, parseTree);

        Assert.That(fakeListener.Meter, Is.EqualTo("4/4"));
    }

    [Test]
    public void ParsesKey()
    {
        var fakeListener = new TestHeaderListener();
        var parseTree = SetupHelpers.SetUpParseTree(TEST_HEADER);
        var walker = new ParseTreeWalker();

        walker.Walk(fakeListener, parseTree);

        Assert.That(fakeListener.Key, Is.EqualTo("C"));
    }

    [Test]
    public void ParsesTempo()
    {
        var fakeListener = new TestHeaderListener();
        var parseTree = SetupHelpers.SetUpParseTree(TEST_HEADER);
        var walker = new ParseTreeWalker();

        walker.Walk(fakeListener, parseTree);

        Assert.That(fakeListener.TempoDesc, Is.EqualTo("1/4=140"));
    }

    [Test]
    public void ParsesComposer()
    {
        var fakeListener = new TestHeaderListener();
        var parseTree = SetupHelpers.SetUpParseTree(TEST_HEADER);
        var walker = new ParseTreeWalker();

        walker.Walk(fakeListener, parseTree);

        Assert.That(fakeListener.Composer, Is.EqualTo("W. Mozart"));
    }
}