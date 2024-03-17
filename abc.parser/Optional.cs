namespace abc.parser;

/// <summary>
/// Represents a type that may or may not have a value. This is helpful
/// as a return type for fallible actions, where we may not always have
/// an actual value to return.
///
/// Essentially, there are two valid states: Some and None. The static
/// factory methods are used to create instances in those states.
/// </summary>
/// <typeparam name="TVal"></typeparam>
public class Optional<TVal>
{
    private readonly TVal? _value;

    private Optional(bool hasData, TVal? value)
    {
        HasData = hasData;
        _value = value;
    }

    /// <summary>
    /// Checks whether this optional type has data.
    /// </summary>
    public bool HasData { get; }

    /// <summary>
    /// Constructs an instance of <see cref="Optional{TVal}"/> that does have data.
    /// </summary>
    /// <param name="value">The data to store in this instance.</param>
    public static Optional<TVal> Some(TVal value)
    {
        return new Optional<TVal>(true, value);
    }

    /// <summary>
    /// Constructs an instance of <see cref="Optional{TVal}"/> that does not
    /// have any associated data.
    /// </summary>
    public static Optional<TVal> None()
    {
        return new Optional<TVal>(false, default);
    }

    /// <summary>
    /// A safe method for attempting to read data from the instance.
    /// </summary>
    /// <param name="value">
    /// If this instance has data, the value will be stored here. If not,
    /// will be set to the type's default value.
    /// </param>
    /// <returns>true if there is data, false if not. Do not try to use <paramref name="value"/> if false</returns>
    public bool TryGetValue(out TVal? value)
    {
        value = HasData ? _value! : default;
        return HasData;
    }
}
