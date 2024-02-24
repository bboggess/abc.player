using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Gets the tempo definition from an ABC header
/// </summary>
public class TempoVisitor : AbcHeaderBaseVisitor<TempoDefinition>
{
    public override TempoDefinition VisitTempoDef([NotNull] AbcHeaderParser.TempoDefContext context)
    {
        var fraction = new FractionVisitor().Visit(context.fraction()) ?? throw new ParseException(context);
        var bpm = int.Parse(context.INT().Symbol.Text);
        return new TempoDefinition(fraction, bpm);
    }
}
