using System.Diagnostics.CodeAnalysis;
using abc.parser.model;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Parses a fraction found in an ABC header. Can be called from other visitors as needed.
/// </summary>
public class FractionVisitor : AbcHeaderBaseVisitor<Ratio>
{
    public override Ratio VisitFraction([NotNull] AbcHeaderParser.FractionContext context)
    {
        var numerator = int.Parse(context.INT(0).Symbol.Text);
        var denominator = int.Parse(context.INT(1).Symbol.Text);

        return new Ratio(numerator, denominator);
    }
}
