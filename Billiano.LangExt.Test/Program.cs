using Billiano.LangExt.Functional;

// Option
Option<int> i = Option.NoValue<int>();


// Result
Result a = Result.Ok();
Result b = new InvalidOperationException();

try
{
    a.ThrowIfFailed();
    b.ThrowIfFailed();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    Console.WriteLine();
}

// Result<int>
Result<int> i = 10;
Result<int> j = Result.Ok(21);
Result<int> k = Result.Fail<int>(new IndexOutOfRangeException());

static int Double(int v) => v + v;
static int GetHashCode(object obj) => obj.GetHashCode();

Console.WriteLine(i.Match(Double, GetHashCode));
Console.WriteLine(j.Match(Double, GetHashCode));
Console.WriteLine(k.Match(Double, GetHashCode));
Console.WriteLine();

if (i.TryGetValue(out var x))
    Console.WriteLine(x);
if (j.TryGetValue(out var y))
    Console.WriteLine(y);
if (k.TryGetValue(out var z))
    Console.WriteLine(z);

Console.WriteLine();

Console.WriteLine(i.GetValueOrDefault());
Console.WriteLine(j.GetValueOrDefault(102));
Console.WriteLine(j.GetValueOrDefault(() => 123));
Console.WriteLine(k.GetValueOrDefault());
Console.WriteLine(k.GetValueOrDefault(213));
Console.WriteLine(k.GetValueOrDefault(() => 12));
Console.WriteLine();

try
{
    i.ThrowIfFailed();
    j.ThrowIfFailed();
    k.ThrowIfFailed();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}