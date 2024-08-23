namespace Billiano.LangExt.Functional;

public interface IResult
{
    public Exception Exception { get; }
    bool IsSuccess { get; }
    bool IsFailed { get; }
}

public interface IResult<T> : IResult
{
    public T Value { get; }
}