namespace Billiano.LangExt.OptionResult;

/// <summary>
/// Represents the result of an operation that can either succeed or fail.
/// </summary>
public interface IResult
{
    /// <summary>
    /// Gets the exception that caused the operation to fail, if any.
    /// </summary>
    public Exception Exception { get; }

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    bool IsFailed { get; }
}

/// <summary>
/// Represents the result of an operation that can either succeed or fail, returning a value.
/// </summary>
/// <typeparam name="T">The type of the result value.</typeparam>
public interface IResult<T> : IResult
{
    /// <summary>
    /// Gets the result value.
    /// </summary>
    public T Value { get; }
}