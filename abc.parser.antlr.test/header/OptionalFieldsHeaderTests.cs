namespace abc.parser.antlr.test.header;

/// <summary>
/// Tests that optional fields can be omitted from the header.
/// </summary>
public class OptionalFieldsHeaderTests
{
    [Test]
    public void NoParserErrorWhenNoOptionalFields()
    {
        var noOptionalFieldsHeader = """
                                    X:1
                                    T:Piece 1
                                    K:C

                                    """;
        var errorDetector = new ParserErrorDetector();
        var parser = SetupHelpers.SetUpParser(noOptionalFieldsHeader, errorDetector);

        // Now let's actually parse the header
        var _ = parser.tuneHeader();

        Assert.That(errorDetector.HasErrors, Is.False);
    }
}
