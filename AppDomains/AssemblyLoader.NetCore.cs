using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;

namespace AppDomains
{
    class AssemblyLoader
    {
        public static int UseAssemblyAndUnload()
        {
            Console.WriteLine($"Math library loaded in default context: { AssemblyLoadContext.Default.Assemblies.Any(a => a.FullName.Contains("Math"))}");

            var newContext = RunCodeInNewLoadContextAndUnload(out int result);
            for (int i = 0; i < 10 && newContext.IsAlive; i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            Console.WriteLine($"All load contexts: {string.Join(", ", AssemblyLoadContext.All.Select(a => a.Name))}");

            return result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static WeakReference RunCodeInNewLoadContextAndUnload(out int result)
        {
            var loadContext = new SampleAssemblyLoadContext("NewContext");
            Console.WriteLine($"Math library loaded in new context: { loadContext.Assemblies.Any(a => a.FullName.Contains("Math"))}");
            var mathLib = loadContext.LoadFromAssemblyName(new AssemblyName("AppDomains"));
            var mbroType = mathLib.GetType("AppDomains.MBROType");
            var method = mbroType.GetMethod("CallHelper");

            var objectFromOtherContext = Activator.CreateInstance(mbroType);
            result = (int)method.Invoke(objectFromOtherContext, new object[] { 4 });

            Console.WriteLine($"Math library loaded in default context: { AssemblyLoadContext.Default.Assemblies.Any(a => a.FullName.Contains("Math"))}");
            Console.WriteLine($"Math library loaded in new context: { loadContext.Assemblies.Any(a => a.FullName.Contains("Math"))}");
            Console.WriteLine($"All load contexts: {string.Join(", ", AssemblyLoadContext.All.Select(a => a.Name))}");

            var reference = new WeakReference(loadContext, true);

            Console.WriteLine("Unloading domain...");
            loadContext.Unload();

            return reference;
        }
    }
}
