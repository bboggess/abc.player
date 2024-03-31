using abc.parser.exception;
using abc.parser.model;

namespace abc.parser.test;

public class KeySignatureTests
{
    public struct AccidentalInfo(IEnumerable<BaseNote> accidentals, int direction)
    {
        public int Direction { get; set; } = direction;
        public IEnumerable<BaseNote> Accidentals { get; set; } = accidentals;
    }

    // For each valid key signature, add the list of accidentals to KeyAccidentals as well.
    // The orders need to be the same for each.
    private static readonly IEnumerable<KeySignature> ValidKeySignatures =
    [
        new KeySignature(new KeyTonic(BaseNote.C, Accidental.Natural), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.G, Accidental.Natural), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.D, Accidental.Natural), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.A, Accidental.Natural), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.E, Accidental.Natural), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.B, Accidental.Natural), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.F, Accidental.Sharp), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.C, Accidental.Sharp), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.F, Accidental.Natural), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.B, Accidental.Flat), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.E, Accidental.Flat), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.A, Accidental.Flat), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.D, Accidental.Flat), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.G, Accidental.Flat), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.C, Accidental.Flat), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.A, Accidental.Natural), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.E, Accidental.Natural), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.B, Accidental.Natural), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.F, Accidental.Sharp), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.C, Accidental.Sharp), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.G, Accidental.Sharp), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.D, Accidental.Sharp), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.A, Accidental.Sharp), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.D, Accidental.Natural), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.G, Accidental.Natural), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.C, Accidental.Natural), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.F, Accidental.Natural), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.B, Accidental.Flat), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.E, Accidental.Flat), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.A, Accidental.Flat), Mode.Minor),
    ];

    private static readonly IEnumerable<AccidentalInfo> KeyAccidentals =
    [
        new([], 0),
        new([BaseNote.F], 1),
        new([BaseNote.F, BaseNote.C], 1),
        new([BaseNote.F, BaseNote.C, BaseNote.G], 1),
        new([BaseNote.F, BaseNote.C, BaseNote.G, BaseNote.D], 1),
        new([BaseNote.F, BaseNote.C, BaseNote.G, BaseNote.D, BaseNote.A], 1),
        new([BaseNote.F, BaseNote.C, BaseNote.G, BaseNote.D, BaseNote.A, BaseNote.E], 1),
        new(
            [BaseNote.F, BaseNote.C, BaseNote.G, BaseNote.D, BaseNote.A, BaseNote.E, BaseNote.B],
            1
        ),
        new([BaseNote.B], -1),
        new([BaseNote.B, BaseNote.E], -1),
        new([BaseNote.B, BaseNote.E, BaseNote.A], -1),
        new([BaseNote.B, BaseNote.E, BaseNote.A, BaseNote.D], -1),
        new([BaseNote.B, BaseNote.E, BaseNote.A, BaseNote.D, BaseNote.G], -1),
        new([BaseNote.B, BaseNote.E, BaseNote.A, BaseNote.D, BaseNote.G, BaseNote.C], -1),
        new(
            [BaseNote.B, BaseNote.E, BaseNote.A, BaseNote.D, BaseNote.G, BaseNote.C, BaseNote.F],
            -1
        ),
        new([], 0),
        new([BaseNote.F], 1),
        new([BaseNote.F, BaseNote.C], 1),
        new([BaseNote.F, BaseNote.C, BaseNote.G], 1),
        new([BaseNote.F, BaseNote.C, BaseNote.G, BaseNote.D], 1),
        new([BaseNote.F, BaseNote.C, BaseNote.G, BaseNote.D, BaseNote.A], 1),
        new([BaseNote.F, BaseNote.C, BaseNote.G, BaseNote.D, BaseNote.A, BaseNote.E], 1),
        new(
            [BaseNote.F, BaseNote.C, BaseNote.G, BaseNote.D, BaseNote.A, BaseNote.E, BaseNote.B],
            1
        ),
        new([BaseNote.B], -1),
        new([BaseNote.B, BaseNote.E], -1),
        new([BaseNote.B, BaseNote.E, BaseNote.A], -1),
        new([BaseNote.B, BaseNote.E, BaseNote.A, BaseNote.D], -1),
        new([BaseNote.B, BaseNote.E, BaseNote.A, BaseNote.D, BaseNote.G], -1),
        new([BaseNote.B, BaseNote.E, BaseNote.A, BaseNote.D, BaseNote.G, BaseNote.C], -1),
        new(
            [BaseNote.B, BaseNote.E, BaseNote.A, BaseNote.D, BaseNote.G, BaseNote.C, BaseNote.F],
            -1
        ),
    ];

    private static readonly IEnumerable<KeySignature> InvalidKeys =
    [
        new KeySignature(new KeyTonic(BaseNote.G, Accidental.Sharp), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.D, Accidental.Sharp), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.A, Accidental.Sharp), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.E, Accidental.Sharp), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.B, Accidental.Sharp), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.F, Accidental.Flat), Mode.Major),
        new KeySignature(new KeyTonic(BaseNote.E, Accidental.Sharp), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.B, Accidental.Sharp), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.D, Accidental.Flat), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.G, Accidental.Flat), Mode.Minor),
        new KeySignature(new KeyTonic(BaseNote.C, Accidental.Flat), Mode.Minor),
    ];

    private static readonly List<BaseNote> AllNotes =
    [
        BaseNote.A,
        BaseNote.B,
        BaseNote.C,
        BaseNote.D,
        BaseNote.E,
        BaseNote.F,
        BaseNote.G,
    ];

    [Test, Sequential]
    public void TestAllValidKeys(
        [ValueSource(nameof(ValidKeySignatures))] KeySignature key,
        [ValueSource(nameof(KeyAccidentals))] AccidentalInfo expectedAccidentals
    )
    {
        var keyApplier = key.ToAccidentalCorrector();

        Assert.Multiple(() =>
        {
            foreach (var note in AllNotes)
            {
                var testPitch = new Pitch(note, 0);

                var result = keyApplier.AdjustPitch(testPitch);

                var expectedPitch = testPitch.Transpose(
                    expectedAccidentals.Accidentals.Contains(note)
                        ? expectedAccidentals.Direction
                        : 0
                );
                Assert.That(result, Is.EqualTo(expectedPitch));
            }
        });
    }

    [Test]
    public void TestAllInvalidKeys([ValueSource(nameof(InvalidKeys))] KeySignature key)
    {
        var action = () => key.ToAccidentalCorrector();

        Assert.That(action, Throws.InstanceOf<InvalidKeyException>());
    }
}
