namespace abc.parser.antlr;

/// <summary>
/// Able to inject parsing context for the header of an ABC file.
/// </summary>
public interface IHeaderContextProvider
{
    /// <summary>
    /// Provides the context for the full ABC header parser rule.
    /// </summary>
    /// <returns>The full context</returns>
    AbcHeaderParser.TuneHeaderContext GetHeaderContext();
}
