namespace abc.parser.antlr.test;

/// <summary>
/// Tests that required header fields are in fact required.
/// </summary>
public partial class RequiredFieldsHeaderTests
{
    private const string HEADER_NO_TITLE = """
                                           X:1
                                           K:C
                                           
                                           """;

    private const string HEADER_NO_KEY = """
                                         X:1
                                         T:Piece 1
                                         
                                         """;

    private const string HEADER_NO_TRACK = """
                                           T:Piece 1
                                           K:C
                                             
                                           """;

    [Test]
    public void ParserErrorIfMissingTitle()
    {
        
        var errorDetector = new ParserErrorDetector();
        var parser = SetupHelpers.SetUpParser(HEADER_NO_TITLE, errorDetector);

        // Now let's actually parse the header
        var _ = parser.abcFile();

        Assert.That(errorDetector.HasErrors, Is.True);
    }

    [Test]
    public void ParserErrorIfMissingKey()
    {

        var errorDetector = new ParserErrorDetector();
        var parser = SetupHelpers.SetUpParser(HEADER_NO_KEY, errorDetector);

        // Now let's actually parse the header
        var _ = parser.abcFile();

        Assert.That(errorDetector.HasErrors, Is.True);
    }

    [Test]
    public void ParserErrorIfMissingTrackNum()
    {

        var errorDetector = new ParserErrorDetector();
        var parser = SetupHelpers.SetUpParser(HEADER_NO_TRACK, errorDetector);

        // Now let's actually parse the header
        var _ = parser.abcFile();

        Assert.That(errorDetector.HasErrors, Is.True);
    }
}
