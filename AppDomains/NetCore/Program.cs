using NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            /* These APIs exist, but throw
                var evidence = new Evidence();
                Console.WriteLine("Creating domain...");
                var domain = AppDomain.CreateDomain("NewDomain");

                var objectFromOtherDomain = domain.CreateInstanceAndUnwrap("NetFx", "Sample.MBROType") as MBROType;
            */

            Console.WriteLine($"Math library loaded in default context: { AssemblyLoadContext.Default.Assemblies.Any(a => a.FullName.Contains("Math"))}");

            var newContext = RunCodeInNewLoadContextAndUnload();
            for (int i = 0; i < 10 && newContext.IsAlive; i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            Console.WriteLine($"All load contexts: {string.Join(", ", AssemblyLoadContext.All.Select(a => a.Name))}");
           
            Console.WriteLine("- Done -");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static WeakReference RunCodeInNewLoadContextAndUnload()
        {
            var loadContext = new SampleAssemblyLoadContext("NewContext");
            Console.WriteLine($"Math library loaded in new context: { loadContext.Assemblies.Any(a => a.FullName.Contains("Math"))}");
            var mathLib = loadContext.LoadFromAssemblyName(new AssemblyName("NetCore"));
            var mbroType = mathLib.GetType("Sample.MBROType");
            var method = mbroType.GetMethod("CallHelper");

            var objectFromOtherContext = Activator.CreateInstance(mbroType);
            method.Invoke(objectFromOtherContext, new object[] { 4 });

            Console.WriteLine($"Math library loaded in default context: { AssemblyLoadContext.Default.Assemblies.Any(a => a.FullName.Contains("Math"))}");
            Console.WriteLine($"Math library loaded in new context: { loadContext.Assemblies.Any(a => a.FullName.Contains("Math"))}");
            Console.WriteLine($"All load contexts: {string.Join(", ", AssemblyLoadContext.All.Select(a => a.Name))}");

            var ret = new WeakReference(loadContext, true);

            Console.WriteLine("Unloading domain...");
            loadContext.Unload();

            return ret;
        }
    }
}
