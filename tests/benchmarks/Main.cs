using BenchmarkDotNet.Running;

namespace FingerprintBuilder.BenchmarkTests
{
    public class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
