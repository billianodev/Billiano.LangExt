using Billiano.LangExt.Functional;

// Option
Option<int> option_novalue = Option.NoValue<int>();
Option<int> option_maybe = Option.Maybe(1000);
Option<int> option_value = Option.Value(1000);

// Result
Result result_ok = Result.Ok();
Result result_fail_implicit = new InvalidOperationException();

try
{
    result_ok.ThrowIfFailed();
    result_fail_implicit.ThrowIfFailed();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    Console.WriteLine();
}

// Result<int>
Result<int> result_t_ok_implicit = 10;
Result<int> result_t_ok = Result.Ok(21);
Result<int> result_t_fail = Result.Fail<int>(new IndexOutOfRangeException());

static int Double(int v) => v + v;
static int GetHashCode(object obj) => obj.GetHashCode();

Console.WriteLine(result_t_ok_implicit.Match(Double, GetHashCode));
Console.WriteLine(result_t_ok.Match(Double, GetHashCode));
Console.WriteLine(result_t_fail.Match(Double, GetHashCode));
Console.WriteLine();

if (result_t_ok_implicit.TryGetValue(out var x))
    Console.WriteLine(x);
if (result_t_ok.TryGetValue(out var y))
    Console.WriteLine(y);
if (result_t_fail.TryGetValue(out var z))
    Console.WriteLine(z);

Console.WriteLine();

Console.WriteLine(result_t_ok_implicit.GetValueOrDefault());
Console.WriteLine(result_t_ok.GetValueOrDefault(102));
Console.WriteLine(result_t_ok.GetValueOrDefault(() => 123));
Console.WriteLine(result_t_fail.GetValueOrDefault());
Console.WriteLine(result_t_fail.GetValueOrDefault(213));
Console.WriteLine(result_t_fail.GetValueOrDefault(() => 12));
Console.WriteLine();

try
{
    result_t_ok_implicit.ThrowIfFailed();
    result_t_ok.ThrowIfFailed();
    result_t_fail.ThrowIfFailed();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}