using System;
using System.Security;

[assembly: AllowPartiallyTrustedCallers]
namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Calling critical method");
            try
            {
                CriticalMethod();
                Console.WriteLine("ERROR - Expected exception not thrown");
            }
            catch (MethodAccessException)
            {
                Console.WriteLine("Caught expected exception for going from Transparent -> Critical code");
            }
            SafeCriticalMethod();
            Console.WriteLine("Done");
        }

        [SecuritySafeCritical]
        private static void SafeCriticalMethod()
        {
            Console.WriteLine("In a safe critical method; calling a critical method");
            CriticalMethod();
        }

        [SecurityCritical]
        private static void CriticalMethod()
        {
            Console.WriteLine("In a critical method");
        }
    }
}
