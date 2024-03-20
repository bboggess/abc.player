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

    /// <summary>
    /// Applies a fallible function to our type if it has a value, flattening out Nones.
    /// </summary>
    /// <typeparam name="TOut">Type to transform our data into</typeparam>
    /// <param name="func">Function to apply to value, if we have one</param>
    /// <returns>
    /// None if this is None or <paramref name="func"/> returns None.
    /// Otherwise holds value of <paramref name="func"/> applied to data
    /// </returns>
    public Optional<TOut> Bind<TOut>(Func<TVal, Optional<TOut>> func)
    {
        if (!HasData)
        {
            return Optional<TOut>.None();
        }

        return func(_value!);
    }
}
