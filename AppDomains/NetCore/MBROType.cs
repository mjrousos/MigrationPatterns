using System;
using System.Runtime.Loader;

namespace Sample
{
    public class MBROType : MarshalByRefObject
    {
        public void CallHelper(int index)
        {
            Console.WriteLine($"[{AssemblyLoadContext.GetLoadContext(this.GetType().Assembly).Name}] Fibonacci number {index}: {MathLib.Math.Fibonacci(index)}");
        }
    }
}
