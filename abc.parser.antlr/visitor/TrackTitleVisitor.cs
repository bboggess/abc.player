using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Parses the title field of the track as a simple string
/// </summary>
public class TrackTitleVisitor : AbcHeaderBaseVisitor<string>
{
    public override string VisitFieldTitle([NotNull] AbcHeaderParser.FieldTitleContext context)
    {
        return new FreeTextVisitor().Visit(context.text());
    }
}
