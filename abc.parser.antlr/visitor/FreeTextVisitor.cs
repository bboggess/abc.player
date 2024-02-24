using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Parses a free text field. It is assumed that when present, these fields should be
/// nonempty. An exception will be thrown if the text is not given.
/// </summary>
public class FreeTextVisitor : AbcHeaderBaseVisitor<string>
{
    public override string VisitText([NotNull] AbcHeaderParser.TextContext context)
    {
        var text = context.GetText();

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ParseException(context);
        }

        return text;
    }
}
