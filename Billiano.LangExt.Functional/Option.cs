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
        return value;
    }

    public static Option<T> Value<T>(T value)
    {
        return value ?? throw new ArgumentNullException(nameof(value));
    }
}

public sealed class Option<T>
{
    internal static readonly Option<T> NoValue = new();

    [NotNull]
    public T? Value { get; }
    public bool HasValue { get; }

    private Option()
    {
    }

    public Option(T? value)
    {
        Value = value;
        HasValue = value is not null;
    }

    public static implicit operator Option<T>(T? value) => new(value);
}
