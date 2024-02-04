namespace abc.parser.antlr.test.header;

/// <summary>
/// Tests that required header fields are in fact required.
/// </summary>
public partial class RequiredFieldsHeaderTests
{
    [Test]
    public void ParserErrorIfMissingTitle()
    {
        var headerWithoutTitle = """
                                 X:1
                                 K:C
                                 
                                 """;
        var errorDetector = new ParserErrorDetector();
        var parser = SetupHelpers.SetUpParser(headerWithoutTitle, errorDetector);

        // Now let's actually parse the header
        var _ = parser.tuneHeader();

        Assert.That(errorDetector.HasErrors, Is.True);
    }

    [Test]
    public void ParserErrorIfMissingKey()
    {
        var headerWithoutKey = """
                                 X:1
                                 T:Piece 1
                                 
                                 """;
        var errorDetector = new ParserErrorDetector();
        var parser = SetupHelpers.SetUpParser(headerWithoutKey, errorDetector);

        // Now let's actually parse the header
        var _ = parser.tuneHeader();

        Assert.That(errorDetector.HasErrors, Is.True);
    }

    [Test]
    public void ParserErrorIfMissingTrackNum()
    {
        var headerWithoutTrack = """
                                 T:Piece 1
                                 K:C
                                 
                                 """;
        var errorDetector = new ParserErrorDetector();
        var parser = SetupHelpers.SetUpParser(headerWithoutTrack, errorDetector);

        // Now let's actually parse the header
        var _ = parser.tuneHeader();

        Assert.That(errorDetector.HasErrors, Is.True);
    }
}
