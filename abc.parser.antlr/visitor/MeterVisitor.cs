using System.Diagnostics.CodeAnalysis;
using abc.parser.antlr.model;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Visitor to handle getting the meter out of an ABC header.
/// </summary>
public class MeterVisitor : AbcHeaderBaseVisitor<TimeSignature>
{
    public override TimeSignature VisitCommonTime([NotNull] AbcHeaderParser.CommonTimeContext context)
    {
        return TimeSignature.FromCommonTime();
    }

    public override TimeSignature VisitCutTime([NotNull] AbcHeaderParser.CutTimeContext context)
    {
        return TimeSignature.FromCutTime();
    }

    public override TimeSignature VisitFractionMeter([NotNull] AbcHeaderParser.FractionMeterContext context)
    {
        var fraction = new FractionVisitor().Visit(context.fraction()) ?? throw new ParseException(context);
        return TimeSignature.FromFractionalTime(fraction);
    }
}
