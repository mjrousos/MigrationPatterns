using System;
using System.Linq;

namespace AppDomains
{
    class AssemblyLoader
    {
        public static int UseAssemblyAndUnload()
        {
            Console.WriteLine("Creating domain...");
            var domain = AppDomain.CreateDomain("NewDomain", new System.Security.Policy.Evidence());

            var objectFromOtherDomain = domain.CreateInstanceAndUnwrap("AppDomains", "AppDomains.MBROType") as MBROType;
            Console.WriteLine($"Math library loaded in current domain: {AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName.Contains("Math"))}");
            Console.WriteLine($"Math library loaded in new domain: {domain.GetAssemblies().Any(a => a.FullName.Contains("Math"))}");
            var result = objectFromOtherDomain.CallHelper(5);
            Console.WriteLine($"Math library loaded in current domain: {AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName.Contains("Math"))}");
            Console.WriteLine($"Math library loaded in new domain: {domain.GetAssemblies().Any(a => a.FullName.Contains("Math"))}");

            Console.WriteLine("Unloading domain...");
            AppDomain.Unload(domain);

            return result;
        }
    }
}
