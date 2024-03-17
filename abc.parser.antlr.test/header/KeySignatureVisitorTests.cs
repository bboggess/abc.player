using abc.parser.antlr.visitor;
using abc.parser.model;
using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.test.header;

public class KeySignatureVisitorTests
{
    private static readonly Dictionary<string, NaturalNote> NoteMap =
        new()
        {
            { "A", NaturalNote.A },
            { "B", NaturalNote.B },
            { "C", NaturalNote.C },
            { "D", NaturalNote.D },
            { "E", NaturalNote.E },
            { "F", NaturalNote.F },
            { "G", NaturalNote.G },
        };

    private static readonly Dictionary<string, Accidental> AccidentalMap =
        new()
        {
            { "", Accidental.Natural },
            { "b", Accidental.Flat },
            { "#", Accidental.Sharp },
        };

    private static readonly Dictionary<string, Mode> ModeMap =
        new() { { "", Mode.Major }, { "m", Mode.Minor }, };

    [Test]
    public void HandlesValidKeys(
        [Values("A", "B", "C", "D", "E", "F", "G")] string note,
        [Values("", "b", "#")] string accidental,
        [Values("", "m")] string mode
    )
    {
        var stringUnderTest = $"K:{note}{accidental}{mode}\n";
        var parser = SetupHeaderHelpers.SetUpParser(stringUnderTest);
        var visitor = new KeySignatureVisitor();

        var outcome = visitor.Visit(parser.fieldKey());

        Assert.Multiple(() =>
        {
            Assert.That(outcome, Is.Not.Null);
            Assert.That(outcome.Tonic.BaseNaturalNote, Is.EqualTo(NoteMap[note]));
            Assert.That(outcome.Tonic.Accidental, Is.EqualTo(AccidentalMap[accidental]));
            Assert.That(outcome.Mode, Is.EqualTo(ModeMap[mode]));
        });
    }

    [Test]
    public void ParserErrorOnInvalidNote(
        [Values(
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z"
        )]
            string key
    )
    {
        var stringUnderTest = $"K:{key}\n";
        var parser = SetupHeaderHelpers.SetUpParser(key);
        var visitor = new KeySignatureVisitor();

        var action = () => visitor.Visit(parser.fieldKey());

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }

    [Test]
    public void ParserErrorOnInvalidMode([Values("p", "~", "dim", "3")] string mode)
    {
        var invalidModeHeader = $"K:C{mode}\n";
        var parser = SetupHeaderHelpers.SetUpParser(invalidModeHeader);
        var visitor = new KeySignatureVisitor();

        var action = () => visitor.Visit(parser.fieldKey());

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }
}
