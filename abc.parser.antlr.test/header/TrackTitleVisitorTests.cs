using abc.parser.antlr.visitor;
using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.test.header;

public class TrackTitleVisitorTests
{
    [Test]
    public void ParseNonEmptyTitle()
    {
        var stringUnderTest = "T:Opus No. 2\n";
        var parser = SetupHelpers.SetUpParser(stringUnderTest);
        var visitor = new TrackTitleVisitor();

        var outcome = visitor.Visit(parser.fieldTitle());

        Assert.That(outcome, Is.EqualTo("Opus No. 2"));
    }

    [Test]
    public void RejectsWhitespaceTitle()
    {
        var stringUnderTest = "T: \t  \n";
        var parser = SetupHelpers.SetUpParser(stringUnderTest);
        var visitor = new TrackTitleVisitor();

        var action = () => visitor.Visit(parser.fieldTitle());

        Assert.That(action, Throws.InstanceOf<ParseException>());
    }

    [Test]
    public void RejectsEmptyTitle()
    {
        var stringUnderTest = "T:\n";
        var parser = SetupHelpers.SetUpParser(stringUnderTest);
        var visitor = new TrackTitleVisitor();

        var action = () => visitor.Visit(parser.fieldTitle());

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }
}
