using abc.parser.antlr.visitor;
using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.test.header;
public class IndexVisitorTests
{
    [Test]
    public void AcceptsNumericIndex([Random(10)] int index)
    {
        var stringUnderTest = $"X:{index}\n";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new IndexVisitor();

        var outcome = visitor.Visit(parser.fieldNumber());

        Assert.That(outcome.Id, Is.EqualTo(index));
    }

    [Test]
    public void RejectsNonNumericIndex()
    {
        var stringUnderTest = "X:index\n";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new IndexVisitor();

        var action = () => visitor.Visit(parser.fieldNumber());

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }
}
