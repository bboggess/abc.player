using System.Diagnostics.CodeAnalysis;
using abc.parser.model;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Visitor to handle getting the meter out of an ABC header.
/// </summary>
public class MeterVisitor : AbcBaseVisitor<TimeSignature>
{
    public override TimeSignature VisitFieldMeter([NotNull] AbcParser.FieldMeterContext context)
    {
        return Visit(context.timeSignature());
    }

    public override TimeSignature VisitCommonTime([NotNull] AbcParser.CommonTimeContext context)
    {
        return TimeSignature.FromCommonTime();
    }

    public override TimeSignature VisitCutTime([NotNull] AbcParser.CutTimeContext context)
    {
        return TimeSignature.FromCutTime();
    }

    public override TimeSignature VisitFractionMeter(
        [NotNull] AbcParser.FractionMeterContext context
    )
    {
        var fraction =
            new FractionVisitor().Visit(context.fraction()) ?? throw new ParseException(context);
        return TimeSignature.FromFractionalTime(fraction);
    }
}
