using abc.parser.parse;
using Antlr4.Runtime.Tree;
using Moq;

namespace abc.parser.antlr.test.body;

public class TuneBodyListenerTests
{
    [Test]
    public void RaisesEvent_BodyNoteEvent_OncePerNote()
    {
        var stringUnderTest = "CCCC";

        var mockBuilder = new Mock<ITuneBodyParser>();

        var listener = new TuneBodyListener(mockBuilder.Object);
        var walker = new ParseTreeWalker();
        var parser = SetupHelpers.SetUpParser(stringUnderTest);

        walker.Walk(listener, parser.tuneBody());

        mockBuilder.Verify(
            m => m.AddNote(It.Is<BodyNoteEvent>(e => e.Pitch == 'C')),
            Times.Exactly(4)
        );
    }

    [Test]
    public void DoesNotRaiseEvent_BodyRestEvent_NoRests()
    {
        var stringUnderTest = "CCCC";

        var mockBuilder = new Mock<ITuneBodyParser>();

        var listener = new TuneBodyListener(mockBuilder.Object);
        var walker = new ParseTreeWalker();
        var parser = SetupHelpers.SetUpParser(stringUnderTest);

        walker.Walk(listener, parser.tuneBody());

        mockBuilder.Verify(m => m.AddRest(It.IsAny<BodyRestEvent>()), Times.Never);
    }

    [Test]
    public void DoesNotRaiseEvent_BodyAccidentalEvent_NoAccidentals()
    {
        var stringUnderTest = "CCCC";

        var mockBuilder = new Mock<ITuneBodyParser>();

        var listener = new TuneBodyListener(mockBuilder.Object);
        var walker = new ParseTreeWalker();
        var parser = SetupHelpers.SetUpParser(stringUnderTest);

        walker.Walk(listener, parser.tuneBody());

        mockBuilder.Verify(m => m.AddAccidental(It.IsAny<BodyAccidentalEvent>()), Times.Never);
    }

    [Test]
    public void RaisesEvent_BodyRestEvent_OncePerRest()
    {
        var stringUnderTest = "zzzz";

        var mockBuilder = new Mock<ITuneBodyParser>();

        var listener = new TuneBodyListener(mockBuilder.Object);
        var walker = new ParseTreeWalker();
        var parser = SetupHelpers.SetUpParser(stringUnderTest);

        walker.Walk(listener, parser.tuneBody());

        mockBuilder.Verify(m => m.AddRest(It.IsAny<BodyRestEvent>()), Times.Exactly(4));
    }

    [Test]
    public void DoesNotRaiseEvent_BodyNoteEvent_OnlyRests()
    {
        var stringUnderTest = "zzzz";

        var mockBuilder = new Mock<ITuneBodyParser>();

        var listener = new TuneBodyListener(mockBuilder.Object);
        var walker = new ParseTreeWalker();
        var parser = SetupHelpers.SetUpParser(stringUnderTest);

        walker.Walk(listener, parser.tuneBody());

        mockBuilder.Verify(m => m.AddNote(It.IsAny<BodyNoteEvent>()), Times.Never);
    }

    [Test]
    public void RaisesEvent_BodyAccidentalEvent(
        [Values("__", "_", "=", "^", "^^")] string accidental
    )
    {
        var stringUnderTest = $"C{accidental}CCC";

        var mockBuilder = new Mock<ITuneBodyParser>();

        var listener = new TuneBodyListener(mockBuilder.Object);
        var walker = new ParseTreeWalker();
        var parser = SetupHelpers.SetUpParser(stringUnderTest);

        walker.Walk(listener, parser.tuneBody());

        mockBuilder.Verify(
            m =>
                m.AddAccidental(
                    It.Is<BodyAccidentalEvent>(e =>
                        e.AccidentalDescriptor == accidental && e.Pitch == 'C'
                    )
                ),
            Times.Once
        );
    }

    [Test]
    public void BodyNoteEvent_NoDuration_PassesEmptyFields()
    {
        var stringUnderTest = "C";

        var mockBuilder = new Mock<ITuneBodyParser>();

        var listener = new TuneBodyListener(mockBuilder.Object);
        var walker = new ParseTreeWalker();
        var parser = SetupHelpers.SetUpParser(stringUnderTest);

        walker.Walk(listener, parser.tuneBody());

        mockBuilder.Verify(
            m =>
                m.AddNote(
                    It.Is<BodyNoteEvent>(e =>
                        e.Length.Numerator == null
                        && e.Length.Denominator == null
                        && !e.Length.IsSpecifiedDenominator
                    )
                ),
            Times.Once
        );
    }

    [Test]
    public void BodyRestEvent_NoDuration_PassesEmptyFields()
    {
        var stringUnderTest = "z";

        var mockBuilder = new Mock<ITuneBodyParser>();

        var listener = new TuneBodyListener(mockBuilder.Object);
        var walker = new ParseTreeWalker();
        var parser = SetupHelpers.SetUpParser(stringUnderTest);

        walker.Walk(listener, parser.tuneBody());

        mockBuilder.Verify(
            m =>
                m.AddRest(
                    It.Is<BodyRestEvent>(e =>
                        e.Length.Numerator == null
                        && e.Length.Denominator == null
                        && !e.Length.IsSpecifiedDenominator
                    )
                ),
            Times.Once
        );
    }

    [Test]
    public void BodyNoteEvent_NoteDuration_HandlesOnlySlash()
    {
        var stringUnderTest = "C/";

        var mockBuilder = new Mock<ITuneBodyParser>();

        var listener = new TuneBodyListener(mockBuilder.Object);
        var walker = new ParseTreeWalker();
        var parser = SetupHelpers.SetUpParser(stringUnderTest);

        walker.Walk(listener, parser.tuneBody());

        mockBuilder.Verify(
            m =>
                m.AddNote(
                    It.Is<BodyNoteEvent>(e =>
                        e.Length.Numerator == null
                        && e.Length.Denominator == null
                        && e.Length.IsSpecifiedDenominator
                    )
                ),
            Times.Once
        );
    }

    [Test]
    public void BodyNoteEvent_NoteDuration_HandlesOnlyNumerator()
    {
        var stringUnderTest = "C2";

        var mockBuilder = new Mock<ITuneBodyParser>();

        var listener = new TuneBodyListener(mockBuilder.Object);
        var walker = new ParseTreeWalker();
        var parser = SetupHelpers.SetUpParser(stringUnderTest);

        walker.Walk(listener, parser.tuneBody());

        mockBuilder.Verify(
            m =>
                m.AddNote(
                    It.Is<BodyNoteEvent>(e =>
                        e.Length.Numerator == 2
                        && e.Length.Denominator == null
                        && !e.Length.IsSpecifiedDenominator
                    )
                ),
            Times.Once
        );
    }

    [Test]
    public void BodyNoteEvent_NoteDuration_HandlesImplicitNumerator()
    {
        var stringUnderTest = "C/2";

        var mockBuilder = new Mock<ITuneBodyParser>();

        var listener = new TuneBodyListener(mockBuilder.Object);
        var walker = new ParseTreeWalker();
        var parser = SetupHelpers.SetUpParser(stringUnderTest);

        walker.Walk(listener, parser.tuneBody());

        mockBuilder.Verify(
            m =>
                m.AddNote(
                    It.Is<BodyNoteEvent>(e =>
                        e.Length.Numerator == null
                        && e.Length.Denominator == 2
                        && e.Length.IsSpecifiedDenominator
                    )
                ),
            Times.Once
        );
    }

    [Test]
    public void BodyNoteEvent_NoteDuration_HandlesImplicitDenominator()
    {
        var stringUnderTest = "C2/";

        var mockBuilder = new Mock<ITuneBodyParser>();

        var listener = new TuneBodyListener(mockBuilder.Object);
        var walker = new ParseTreeWalker();
        var parser = SetupHelpers.SetUpParser(stringUnderTest);

        var tokens = parser.TokenStream;

        walker.Walk(listener, parser.tuneBody());

        mockBuilder.Verify(
            m =>
                m.AddNote(
                    It.Is<BodyNoteEvent>(e =>
                        e.Length.Numerator == 2
                        && e.Length.Denominator == null
                        && e.Length.IsSpecifiedDenominator
                    )
                ),
            Times.Once
        );
    }

    [Test]
    public void BodyNoteEvent_NoteDuration_HandlesFullSpec()
    {
        var stringUnderTest = "C1/4";

        var mockBuilder = new Mock<ITuneBodyParser>();

        var listener = new TuneBodyListener(mockBuilder.Object);
        var walker = new ParseTreeWalker();
        var parser = SetupHelpers.SetUpParser(stringUnderTest);

        var tokens = parser.TokenStream;

        walker.Walk(listener, parser.tuneBody());

        mockBuilder.Verify(
            m =>
                m.AddNote(
                    It.Is<BodyNoteEvent>(e =>
                        e.Length.Numerator == 1
                        && e.Length.Denominator == 4
                        && e.Length.IsSpecifiedDenominator
                    )
                ),
            Times.Once
        );
    }
}
