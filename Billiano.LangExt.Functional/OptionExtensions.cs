using System.Diagnostics.CodeAnalysis;

namespace Billiano.LangExt.Functional;

public static class OptionExtensions
{
    public static T? GetValueOrDefault<T>(this Option<T> option)
    {
        if (!option.HasValue)
        {
            return default;
        }

        return option.Value;
    }

    public static T GetValueOrDefault<T>(this Option<T> option, T defaultValue)
    {
        if (!option.HasValue)
        {
            return defaultValue;
        }

        return option.Value;
    }

    public static T GetValueOrDefault<T>(this Option<T> option, Func<T> func)
    {
        if (!option.HasValue)
        {
            return func();
        }

        return option.Value;
    }

    public static bool TryGetValue<T>(this Option<T> option, [NotNullWhen(true)] out T value)
    {
        value = option.Value;
        return option.HasValue;
    }
}
