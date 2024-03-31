using abc.parser.model;

namespace abc.parser.test;

public class PitchTests
{
    private static readonly IEnumerable<BaseNote> AllNotes = BaseNote.AllNotes();

    [Test]
    public void ChromaticNote_RandomOctaves(
        [ValueSource(nameof(AllNotes))] BaseNote note,
        [Range(-3, 3)] int octave
    )
    {
        var pitch = new Pitch(note, octave);

        var result = pitch.ChromaticNote;

        Assert.That(result, Is.EqualTo(note));
    }
}
