namespace abc.parser.antlr.test;

/// <summary>
/// Tests that optional fields can be omitted from the header.
/// </summary>
public class OptionalFieldsHeaderTests
{
    private const string NO_OPTIONAL_FIELDS_HEADER = """
                                                    X:1
                                                    T:Piece 1
                                                    K:C

                                                    """;

    [Test]
    public void NoParserErrorWhenNoOptionalFields()
    {
        var errorDetector = new ParserErrorDetector();
        var parser = SetupHelpers.SetUpParser(NO_OPTIONAL_FIELDS_HEADER, errorDetector);

        // Now let's actually parse the header
        var _ = parser.abcFile();

        Assert.That(errorDetector.HasErrors, Is.False);
    }
}
