namespace Billiano.LangExt.Functional.OptionResult;

/// <summary>
/// This static class contains extension methods for converting between <see cref="Result"/> objects and <see cref="Option{T}"/> objects.
/// </summary>
public static class OptionResultExtensions
{
    /// <summary>
    /// Converts a <see cref="Result{T}"/> object to an <see cref="Option{T}"/> object.
    /// </summary>
    public static Option<T> ToOption<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Option.Some(result.Value);
        }

        return Option.None<T>();
    }

    /// <summary>
    /// Converts a <see cref="Option{T}"/> object to an <see cref="Result{T}"/> object.
    /// </summary>
    public static Result<T> ToResult<T>(this Option<T> option, Exception exception)
    {
        if (option.HasValue)
        {
            return option.Value;
        }

        return Result.Fail<T>(exception);
    }

    /// <summary>
    /// Converts a <see cref="Option{T}"/> object to an <see cref="Result{T}"/> object.
    /// </summary>
    public static Result<T> ToResult<T>(this Option<T> option, Func<Exception> func)
    {
        if (option.HasValue)
        {
            return option.Value;
        }

        var exception = func();
        return Result.Fail<T>(exception);
    }
}
