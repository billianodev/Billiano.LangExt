using System.Runtime.CompilerServices;

namespace Billiano.LangExt.OptionResult;

/// <summary>
/// Provides static methods for creating Option.
/// </summary>
public static class Option
{
    /// <summary>
    /// Returns an empty <see cref="Option{T}"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> None<T>() => default;

    /// <summary>
    /// Creates a new <see cref="Option{T}"/> instance from a nullable value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Maybe<T>(T? value) => new(value);

    /// <summary>
    /// Creates a new <see cref="Option{T}"/> instance containing the specified value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Some<T>(T value) => new(value, true);
}

/// <summary>
/// Represents an optional value.
/// </summary>
public readonly struct Option<T> : IOption<T>
{
    private readonly T? _value;

    internal Option(T? value)
    {
        _value = value;
        HasValue = value is not null;
    }

    internal Option(T? value, bool hasValue)
    {
        _value = value;
        HasValue = hasValue;
    }

    /// <summary>
    /// Gets the value of the Option.
    /// </summary>
    /// <exception cref="InvalidOperationException"/>
    public T Value => HasValue ? _value! : throw new InvalidOperationException();

    /// <summary>
    /// Gets a value indicating whether the Option has a value.
    /// </summary>
    public bool HasValue { get; }

    /// <summary>
    /// Implicitly converts a nullable value to an Option.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Option<T>(T? value) => new(value);
}
