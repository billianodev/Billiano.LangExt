using System.Runtime.CompilerServices;

namespace Billiano.LangExt.Functional;

public readonly struct Option
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> None<T>() => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Maybe<T>(T? value) => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Some<T>(T value) => new(value, true);
}

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

    public T Value => HasValue ? _value! : throw new InvalidOperationException();
    public bool HasValue { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Option<T>(T? value) => new(value);
}
