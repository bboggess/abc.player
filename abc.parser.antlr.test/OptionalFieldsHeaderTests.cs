namespace abc.parser.antlr.test;

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
        var _ = parser.abcHeader();

        Assert.That(errorDetector.HasErrors, Is.False);
    }
}
