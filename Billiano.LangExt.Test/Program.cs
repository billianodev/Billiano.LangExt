using Billiano.LangExt.Test.Samples.UserServiceSample;

namespace Billiano.LangExt.Test;

public static class Program
{
    private static readonly List<SampleInfo> _samples = [];

    static Program()
    {
        RegisterSample<UserServiceSample>();
        RegisterSample<UserServiceSampleClassic>();
    }

    private static void Main()
    {
    start:
        try
        {
            Console.Clear();
            Console.WriteLine("Select which sample to run:");

            for (var i = 0; i < _samples.Count; i++)
            {
                var sample = _samples[i];
                Console.WriteLine("{0,3}. {1}", i + 1, sample.Title);
            }

            Console.Write(">>> ");
            var str = Console.ReadLine();

            if (int.TryParse(str, out var index))
            {
                _samples[index - 1].Sample.RunSample();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            Console.WriteLine();
            Console.WriteLine("End of sample. Press ENTER to return...");
            Console.ReadLine();
        }

        goto start;
    }

    private static void RegisterSample<T>() where T : ISample, new()
    {
        _samples.Add(new SampleInfo(typeof(T).Name, new T()));
    }
}
