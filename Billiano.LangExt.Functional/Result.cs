using System.Diagnostics.CodeAnalysis;

namespace Billiano.LangExt.Functional;

public class Result
{
    [NotNull]
    public Exception? Exception { get; }

    public bool IsSuccess { get; }
    public bool IsFailed => !IsSuccess;

    public Result()
    {
        IsSuccess = true;
    }

    public Result(Exception exception)
    {
        if (exception is null)
        {
            throw new ArgumentNullException(nameof(exception));
        }

        Exception = exception;
        IsSuccess = false;
    }

    public static Result Ok() => new();
    public static Result Fail(Exception ex) => new(ex);

    public static Result<T> Ok<T>(T value) => new(value);
    public static Result<T> Fail<T>(Exception ex) => new(ex);

    public static implicit operator Result(Exception ex) => new(ex);
}

public class Result<T> : Result
{
    [NotNull]
    public T? Value { get; }

    public Result(T value)
    {
        Value = value;
    }

    public Result(Exception exception) : base(exception)
    {
    }

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Exception ex) => new(ex);
}