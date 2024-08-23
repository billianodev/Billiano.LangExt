using Billiano.LangExt.Test.Samples.UserServiceSample;

namespace Billiano.LangExt.Test;

public static class Program
{
    private static readonly List<ISample> _samples = [];

    static Program()
    {
        RegisterSample<UserServiceSample>();
    }

    private static void Main()
    {
        while (true)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Select which sample to run:");

                for (var i = 0; i < _samples.Count; i++)
                {
                    var sample = _samples[i];
                    Console.WriteLine("{0,3}. {1}", i + 1, sample.GetType().Name);
                }

                Console.Write(">>> ");
                var str = Console.ReadLine();

                if (int.TryParse(str, out var index))
                {
                    _samples[index - 1].RunSample();
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
        }
    }

    private static void RegisterSample<T>() where T : ISample, new()
    {
        _samples.Add(new T());
    }
}
