using abc.parser.antlr.visitor;
using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.test.header;
public class ComposerVisitorTests
{
    [Test]
    public void ParseNonEmptyName()
    {
        var stringUnderTest = "C:my composer's name\n";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new ComposerVisitor();

        var outcome = visitor.Visit(parser.fieldComposer());

        Assert.That(outcome.Name, Is.EqualTo("my composer's name"));
    }

    [Test]
    public void RejectsWhitespaceName()
    {
        var stringUnderTest = "C: \t  \n";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new ComposerVisitor();

        var action = () => visitor.Visit(parser.fieldComposer());

        Assert.That(action, Throws.InstanceOf<ParseException>());
    }

    [Test]
    public void RejectsEmptyName()
    {
        var stringUnderTest = "C:\n";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new ComposerVisitor();

        var action = () => visitor.Visit(parser.fieldComposer());

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }
}
