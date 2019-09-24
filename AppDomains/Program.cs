using System;
using System.Security.Policy;

namespace AppDomains
{
    class Program
    {
        static void Main(string[] args)
        {
            var evidence = new Evidence();
            Console.WriteLine($"Current AppDomain name: {AppDomain.CurrentDomain.FriendlyName}");
            Console.WriteLine($"Current AppDomain base directory: {AppDomain.CurrentDomain.BaseDirectory}");
            Console.WriteLine($"Current AppDomain target framework: {AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName}");
            Console.WriteLine();

            var result = AssemblyLoader.UseAssemblyAndUnload();
            Console.WriteLine($"Result calculated in plugin: {result}");

            Console.WriteLine("- Done -");
        }
    }
}
