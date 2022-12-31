using BenchmarkDotNet.Running;

namespace FingerprintBuilder.BenchmarkTests
{
    public class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<ModelToHex>();
        }
    }
}
