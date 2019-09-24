using System;
using System.Runtime.CompilerServices;

namespace AppDomains
{
    public class MBROType : MarshalByRefObject
    {
        public int CallHelper(int index)
        {
            return PrivateCallHelper(index);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public int PrivateCallHelper(int index)
        {
            var result = MathLib.Math.Fibonacci(index);
            var domainName =
#if NET472
                AppDomain.CurrentDomain.FriendlyName;
#elif NETCOREAPP
                System.Runtime.Loader.AssemblyLoadContext.GetLoadContext(this.GetType().Assembly).Name;
#endif

            Console.WriteLine($"[{domainName}] Fibonacci number {index}: {result}");

            return result;
        }
    }
}
