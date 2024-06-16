using Antlr4.Runtime.Misc;

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
        var parser = SetupHelpers.SetUpParser(noOptionalFieldsHeader);

        var action = () =>
        {
            _ = parser.tuneHeader();
        };

        Assert.That(action, Throws.Nothing);
    }
}
