using BenchmarkDotNet.Running;
using System.Reflection;

namespace ImageHash.Bench
{
    public class Program
    {
        public static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
