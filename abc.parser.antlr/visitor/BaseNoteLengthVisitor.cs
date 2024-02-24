using abc.parser.antlr.model;
using Antlr4.Runtime.Misc;

namespace abc.parser.antlr.visitor;

/// <summary>
/// Parsing the note length to use as the base for all notes in the body
/// </summary>
public class BaseNoteLengthVisitor : AbcHeaderBaseVisitor<Ratio>
{
    public override Ratio VisitFieldLength([NotNull] AbcHeaderParser.FieldLengthContext context)
    {
        return new FractionVisitor().Visit(context.fraction()) ?? throw new ParseException(context);
    }
}
