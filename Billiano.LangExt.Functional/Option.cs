using System.Diagnostics.CodeAnalysis;

namespace Billiano.LangExt.Functional;

public static class Option
{
    public static Option<T> NoValue<T>()
    {
        return Option<T>.NoValue;
    }

    public static Option<T> Maybe<T>(T? value)
    {
        return new(value);
    }

    public static Option<T> Value<T>(T value)
    {
        return new(value ?? throw new ArgumentNullException(nameof(value)));
    }
}

public sealed class Option<T>
{
    internal static readonly Option<T> NoValue = new();

#if NETSTANDARD2_1_OR_GREATER
    [NotNull]
#endif
    public T? Value { get; }

#if NET6_0_OR_GREATER
    [MemberNotNullWhen(true, nameof(Value))]
#endif
    public bool HasValue { get; }

    internal Option()
    {
    }

    internal Option(T? value)
    {
        Value = value;
        HasValue = value is not null;
    }

    public static implicit operator Option<T>(T? value) => new(value);
}
