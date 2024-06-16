using abc.parser.model;
using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Parse the track index field from an ABC header
/// </summary>
public class IndexVisitor : AbcBaseVisitor<TrackIndex>
{
    public override TrackIndex VisitFieldNumber([NotNull] AbcParser.FieldNumberContext context)
    {
        var index = int.Parse(context.INT().Symbol.Text);
        return new TrackIndex(index);
    }
}
