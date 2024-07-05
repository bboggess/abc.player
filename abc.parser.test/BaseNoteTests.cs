using abc.parser.model;

namespace abc.parser.test;

public class BaseNoteTests
{
    private static readonly List<BaseNote> AllNaturals =
    [
        BaseNote.A,
        BaseNote.B,
        BaseNote.C,
        BaseNote.D,
        BaseNote.E,
        BaseNote.F,
        BaseNote.G,
    ];

    private static readonly List<BaseNote> AllAccidentals =
    [
        BaseNote.ASharp,
        BaseNote.CSharp,
        BaseNote.DSharp,
        BaseNote.FSharp,
        BaseNote.GSharp,
    ];

    [Test, Sequential]
    public void FactoryMethod(
        [Values('A', 'B', 'C', 'D', 'E', 'F', 'G')] char input,
        [ValueSource(nameof(AllNaturals))] BaseNote expected
    )
    {
        var result = BaseNote.FromChar(input);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void IsNatural_NaturalNotes_True([ValueSource(nameof(AllNaturals))] BaseNote note)
    {
        Assert.That(note.IsNatural);
    }

    [Test]
    public void IsNatural_NonNaturalNotes_False([ValueSource(nameof(AllAccidentals))] BaseNote note)
    {
        Assert.That(note.IsNatural, Is.False);
    }

    [Test]
    public void ApplyAccidental_Sharp()
    {
        var startingNote = BaseNote.C;

        var result = startingNote.ApplyAccidental(Accidental.Sharp);

        Assert.That(result, Is.EqualTo(BaseNote.CSharp));
    }

    [Test]
    public void ApplyAccidental_Flat()
    {
        var startingNote = BaseNote.C;

        var result = startingNote.ApplyAccidental(Accidental.Flat);

        Assert.That(result, Is.EqualTo(BaseNote.B));
    }

    [Test]
    public void ApplyAccidental_Natural()
    {
        var startingNote = BaseNote.C;

        var result = startingNote.ApplyAccidental(Accidental.Natural);

        Assert.That(result, Is.EqualTo(BaseNote.C));
    }

    [Test]
    public void DistanceFrom_Positive()
    {
        var startingNote = BaseNote.C;

        var result = startingNote.DistanceFrom(BaseNote.D);

        Assert.That(result, Is.EqualTo(-2));
    }

    [Test]
    public void DistanceFrom_Negative()
    {
        var startingNote = BaseNote.C;

        var result = startingNote.DistanceFrom(BaseNote.A);

        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    public void DistanceFrom_Zero()
    {
        var startingNote = BaseNote.C;

        var result = startingNote.DistanceFrom(BaseNote.C);

        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void Transpose_PositiveNumber_MovesUp()
    {
        var startingNote = BaseNote.C;

        var result = startingNote.Transpose(2);

        Assert.That(result, Is.EqualTo(BaseNote.D));
    }

    [Test]
    public void Transpose_FullOctave_ReturnsSameNote()
    {
        var startingNote = BaseNote.C;

        var result = startingNote.Transpose(12);

        Assert.That(result, Is.EqualTo(startingNote));
    }

    [Test]
    public void Transpose_MoreThanFullOctave_ReturnsCorrectNote()
    {
        var startingNote = BaseNote.C;

        var result = startingNote.Transpose(14);

        Assert.That(result, Is.EqualTo(BaseNote.D));
    }

    [Test]
    public void Transpose_NegativeValue_StepsDown()
    {
        var startingNote = BaseNote.C;

        var result = startingNote.Transpose(-2);

        Assert.That(result, Is.EqualTo(BaseNote.ASharp));
    }

    [Test]
    public void Transpose_NegativeValue_WrapsCorrectly()
    {
        var startingNote = BaseNote.C;

        var result = startingNote.Transpose(-5);

        Assert.That(result, Is.EqualTo(BaseNote.G));
    }

    [Test]
    public void Transpose_NegativeFullOctave_BehavesCorrectly()
    {
        var startingNote = BaseNote.C;

        var result = startingNote.Transpose(-14);

        Assert.That(result, Is.EqualTo(BaseNote.ASharp));
    }
}
