using abc.parser.antlr.model;
using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Parse the track index field from an ABC header
/// </summary>
public class IndexVisitor : AbcHeaderBaseVisitor<TrackIndex>
{
    public override TrackIndex VisitFieldNumber(
        [NotNull] AbcHeaderParser.FieldNumberContext context
    )
    {
        var index = int.Parse(context.INT().Symbol.Text);
        return new TrackIndex(index);
    }
}
