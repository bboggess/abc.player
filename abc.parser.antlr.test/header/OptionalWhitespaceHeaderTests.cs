using System.Text;

namespace abc.parser.antlr.test.header;

/// <summary>
/// Tests that header fields allow optional whitespace
/// </summary>
public class OptionalWhitespaceHeaderTests
{
    [Test]
    public void AllowSpaceAfterFields()
    {
        var headerWithSpaces = """
            X: 1
            T: Piece 1
            M: 4/4
            L: 1/4
            Q: 1/4=140
            K: C

            """;
        var parser = SetupHelpers.SetUpParser(headerWithSpaces);

        var action = () =>
        {
            _ = parser.tuneHeader();
        };

        Assert.That(action, Throws.Nothing);
    }

    [Test]
    public void AllowTabAfterFields()
    {
        var builder = new StringBuilder();
        builder.AppendLine("X:\t1");
        builder.AppendLine("T:\tPiece 1");
        builder.AppendLine("M:\t4/4");
        builder.AppendLine("L:\t1/4");
        builder.AppendLine("Q:\t1/4=140");
        builder.AppendLine("K:\tC");

        var parser = SetupHelpers.SetUpParser(builder.ToString());

        var action = () =>
        {
            _ = parser.tuneHeader();
        };

        Assert.That(action, Throws.Nothing);
    }
}
