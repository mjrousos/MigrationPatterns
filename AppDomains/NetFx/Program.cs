using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var evidence = new Evidence();
            Console.WriteLine("Creating domain...");
            var domain = AppDomain.CreateDomain("NewDomain", evidence);

            var objectFromOtherDomain = domain.CreateInstanceAndUnwrap("NetFx", "Sample.MBROType") as MBROType;
            Console.WriteLine($"Math library loaded in current domain: {AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName.Contains("Math"))}");
            Console.WriteLine($"Math library loaded in new domain: {domain.GetAssemblies().Any(a => a.FullName.Contains("Math"))}");
            objectFromOtherDomain.CallHelper(5);
            Console.WriteLine($"Math library loaded in current domain: {AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName.Contains("Math"))}");
            Console.WriteLine($"Math library loaded in new domain: {domain.GetAssemblies().Any(a => a.FullName.Contains("Math"))}");

            Console.WriteLine("Unloading domain...");
            AppDomain.Unload(domain);
            Console.WriteLine("- Done -");
        }
    }
}
