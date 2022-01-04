using BenchmarkDotNet.Running;

namespace Perceptash.Benchmarks;

public class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<CalculateHash>();
    }
}