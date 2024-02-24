using Antlr4.Runtime;

namespace abc.parser.antlr;

/// <summary>
/// An Exception thrown when parsing ABC file fails for any reason.
/// </summary>
public class ParseException : Exception
{
    /// <summary>
    /// For use when we hit an error in the context of a specific rule.
    /// </summary>
    /// <param name="context">The rule we were in when parsing failed.</param>
    public ParseException(ParserRuleContext context)
        : base(context.GetText()) { }
}
