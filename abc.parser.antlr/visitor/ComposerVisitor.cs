using abc.parser.model;
using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Parses the composer field in an ABC header
/// </summary>
public class ComposerVisitor : AbcHeaderBaseVisitor<Composer>
{
    public override Composer VisitFieldComposer(
        [NotNull] AbcHeaderParser.FieldComposerContext context
    )
    {
        var composerName = new FreeTextVisitor().Visit(context.text());
        return new Composer(composerName);
    }
}
