using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.test.header;
public class KeySignatureVisitorTests
{
    private static readonly Dictionary<string, BaseNote> NoteMap = new()
    {
        { "A", BaseNote.A },
        { "B", BaseNote.B },
        { "C", BaseNote.C },
        { "D", BaseNote.D },
        { "E", BaseNote.E },
        { "F", BaseNote.F },
        { "G", BaseNote.G },
    };

    private static readonly Dictionary<string, Accidental> AccidentalMap = new()
    {
        { "", Accidental.Natural },
        { "b", Accidental.Flat },
        { "#", Accidental.Sharp },
    };

    private static readonly Dictionary<string, Mode> ModeMap = new()
    {
        { "", Mode.Major },
        { "m", Mode.Minor },
    };

    [Test]
    public void HandlesValidKeys([Values("A", "B", "C", "D", "E", "F", "G")] string note,
        [Values("", "b", "#")] string accidental,
        [Values("", "m")] string mode)
    {
        var stringUnderTest = $"{note}{accidental}{mode}";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new KeySignatureVisitor();

        var outcome = visitor.Visit(parser.keySignature());

        Assert.Multiple(() =>
        {
            Assert.That(outcome, Is.Not.Null);
            Assert.That(outcome.Tonic.BaseNote, Is.EqualTo(NoteMap[note]));
            Assert.That(outcome.Tonic.Accidental, Is.EqualTo(AccidentalMap[accidental]));
            Assert.That(outcome.Mode, Is.EqualTo(ModeMap[mode]));
        });
    }

    [Test]
    public void ParserErrorOnInvalidNote(
        [Values("H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")] string key
    )
    {
        var parser = SetupHeaderHelpers.SetUpParser(key);
        var visitor = new KeySignatureVisitor();

        var action = () => visitor.Visit(parser.keySignature());

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }

    [Test]
    public void ParserErrorOnInvalidMode([Values("p", "~", "dim", "3")] string mode)
    {
        var invalidModeHeader = $"K:C{mode}\n";
        var parser = SetupHeaderHelpers.SetUpParser(invalidModeHeader);
        var visitor = new KeySignatureVisitor();

        var action = () => visitor.Visit(parser.keySignature());

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }
}
