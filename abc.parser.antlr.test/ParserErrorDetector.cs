using Antlr4.Runtime;

namespace abc.parser.antlr.test;

/// <summary>
/// Used to detect parser errors. Attach this to a parser using <see cref="Recognizer{Symbol, ATNInterpreter}.AddErrorListener(IAntlrErrorListener{Symbol})"/>.
/// Then after parsing, <see cref="HasErrors"/> will be true if and only if there are parse errors.
/// </summary>
internal class ParserErrorDetector : IAntlrErrorListener<IToken>
{
    public bool HasErrors { get; private set; }

    public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        HasErrors = true;
    }
}