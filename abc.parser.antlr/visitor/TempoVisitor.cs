using abc.parser.model;
using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Gets the tempo definition from an ABC header
/// </summary>
public class TempoVisitor : AbcBaseVisitor<TempoDefinition>
{
    public override TempoDefinition VisitFieldTempo([NotNull] AbcParser.FieldTempoContext context)
    {
        return Visit(context.tempoDef());
    }

    public override TempoDefinition VisitTempoDef([NotNull] AbcParser.TempoDefContext context)
    {
        var fraction =
            new FractionVisitor().Visit(context.fraction()) ?? throw new ParseException(context);
        var bpm = int.Parse(context.INT().Symbol.Text);
        return new TempoDefinition(fraction, bpm);
    }
}
