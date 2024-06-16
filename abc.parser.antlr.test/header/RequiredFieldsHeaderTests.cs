using Antlr4.Runtime.Misc;

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
        var parser = SetupHelpers.SetUpParser(headerWithoutTitle);

        var action = () =>
        {
            _ = parser.tuneHeader();
        };

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }

    [Test]
    public void ParserErrorIfMissingKey()
    {
        var headerWithoutKey = """
            X:1
            T:Piece 1

            """;
        var parser = SetupHelpers.SetUpParser(headerWithoutKey);

        var action = () =>
        {
            _ = parser.tuneHeader();
        };

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }

    [Test]
    public void ParserErrorIfMissingTrackNum()
    {
        var headerWithoutTrack = """
            T:Piece 1
            K:C

            """;
        var parser = SetupHelpers.SetUpParser(headerWithoutTrack);

        var action = () =>
        {
            _ = parser.tuneHeader();
        };

        Assert.That(action, Throws.InstanceOf<ParseCanceledException>());
    }
}
