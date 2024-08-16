using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Billiano.LangExt.Functional;
public static class ResultExtensions
{
    public static TOut Match<T, TOut>(this Result<T> result, Func<T, TOut> success, Func<Exception, TOut> fail)
    {
        if (result.IsFailed)
        {
            return fail(result.Exception);
        }

        return success(result.Value);
    }

    public static T? GetValueOrDefault<T>(this Result<T> result)
    {
        if (result.IsFailed)
        {
            return default;
        }

        return result.Value;
    }

    public static T GetValueOrDefault<T>(this Result<T> result, T defaultValue)
    {
        if (result.IsFailed)
        {
            return defaultValue;
        }

        return result.Value;
    }

    public static T GetValueOrDefault<T>(this Result<T> result, Func<T> func)
    {
        if (result.IsFailed)
        {
            return func();
        }

        return result.Value;
    }

    public static bool TryGetValue<T>(this Result<T> result, [NotNullWhen(true)] out T? value)
    {
        if (result.IsFailed)
        {
            value = default;
            return false;
        }

        value = result.Value;
        return true;
    }

    public static bool TryGetException<T>(this Result<T> result, [NotNullWhen(true)] out T? value)
    {
        if (result.IsFailed)
        {
            value = default;
            return false;
        }

        value = result.Value;
        return true;
    }

    [DebuggerNonUserCode]
    public static void ThrowIfFailed(this Result result)
    {
        if (result.IsFailed)
        {
            throw result.Exception;
        }
    }
}
