using System;
namespace Sample
{
    public class MBROType : MarshalByRefObject
    {
        public void CallHelper(int index)
        {
            Console.WriteLine($"[{AppDomain.CurrentDomain.FriendlyName}] Fibonacci number {index}: {MathLib.Math.Fibonacci(index)}");
        }
    }
}
