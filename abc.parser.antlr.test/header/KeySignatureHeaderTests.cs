using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace abc.parser.antlr.test.header;

/// <summary>
/// Test parsing for key signature field in header.
/// </summary>
public class KeySignatureHeaderTests
{
    [Test]
    public void AcceptsValidSignatures(
        [Values("A", "B", "C", "D", "E", "F", "G")] string note,
        [Values("", "#", "b")] string accidental,
        [Values("", "m")] string mode
    )
    {
        var keyHeaderField = $"K:{note}{accidental}{mode}\n";
        var fakeListener = new TestHeaderListener();

        var parser = SetupHeaderHelpers.SetUpParser(keyHeaderField);
        var parseTree = parser.fieldKey();
        var walker = new ParseTreeWalker();

        walker.Walk(fakeListener, parseTree);

        var expectedKey = $"{note}{accidental}{mode}";
        Assert.That(fakeListener.Key, Is.EqualTo(expectedKey));
    }

    [Test]
    public void ParserErrorOnInvalidNote(
        [Values("H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")] string key
    )
    {
        var invalidNoteHeader = $"K:{key}\n";
        var parser = SetupHeaderHelpers.SetUpParser(invalidNoteHeader);

        var action = () => { _ = parser.fieldKey(); };

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }

    [Test]
    public void ParserErrorOnInvalidMode([Values("p", "~", "dim", "3")] string mode)
    {
        var invalidModeHeader = $"K:C{mode}\n";
        var parser = SetupHeaderHelpers.SetUpParser(invalidModeHeader);

        var action = () => { _ = parser.fieldKey(); };

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }
}
