namespace Billiano.LangExt.OptionResult;

/// <summary>
/// This static class contains extension methods for converting between <see cref="Result"/> objects and <see cref="Option{T}"/> objects.
/// </summary>
public static class OptionResultExtensions
{
    /// <summary>
    /// Converts a <see cref="Result"/> object to an <see cref="Option{T}"/> object.
    /// </summary>
    public static Option<T> ToOption<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Option.Some(result.Value);
        }

        return Option.None<T>();
    }
}
