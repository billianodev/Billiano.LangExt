using System.Diagnostics.CodeAnalysis;

namespace Billiano.LangExt.Functional;

public static class Option
{
    public static Option<T> NoValue<T>()
    {
        return new Option<T>();
    }

    public static Option<T> Maybe<T>(T? value)
    {
        return new Option<T>(value);
    }

    public static Option<T> Value<T>(T value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return new Option<T>(value);
    }
}

public sealed class Option<T>
{
    [NotNull]
    public T? Value { get; }
    public bool HasValue { get; }

    public Option()
    {
    }

    public Option(T? value)
    {
        Value = value;
        HasValue = value is not null;
    }

    public static implicit operator Option<T>(T value) => new(value);
}
