namespace Billiano.LangExt.Functional;

public interface IOption<T>
{
    T Value { get; }
    bool HasValue { get; }
}