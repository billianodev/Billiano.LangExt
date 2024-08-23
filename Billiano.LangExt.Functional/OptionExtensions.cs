namespace Billiano.LangExt.Functional;

public static class OptionExtensions
{
    public static Option<T> Then<T>(this Option<T> option, Action<T> action)
    {
        if (option.HasValue)
        {
            action(option.Value);
        }

        return option;
    }

    public static Option<TOut> Then<T, TOut>(this Option<T> option, Func<T, TOut> func)
    {
        return option.HasValue ? func(option.Value) : default;
    }

    public static Option<T> Or<T>(this Option<T> option, T defaultValue)
    {
        return option.HasValue ? option : defaultValue;
    }

    public static Option<T> Or<T>(this Option<T> option, Func<T> func)
    {
        return option.HasValue ? option : func();
    }

    public static T? GetValueOrDefault<T>(this Option<T> option)
    {
        return option.HasValue ? option.Value : default;
    }

    public static T GetValueOrDefault<T>(this Option<T> option, T defaultValue)
    {
        return option.HasValue ? option.Value : defaultValue;
    }

    public static T GetValueOrDefault<T>(this Option<T> option, Func<T> func)
    {
        return option.HasValue ? option.Value : func();
    }
}
