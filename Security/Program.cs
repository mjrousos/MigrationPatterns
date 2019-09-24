using System;
using System.Security;

[assembly: AllowPartiallyTrustedCallers]
namespace SecuritySample
{
    class Program
    {
        static void Main()
        {
            UseCAS();
            UseTransparency();
        }

        [SecuritySafeCritical]
        private static void UseCAS()
        {
            ColoredWriteLine("Testing CAS", ConsoleColor.Cyan);
            PermissionSet ps = new PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);
            ps.Demand();
            Console.WriteLine("PermissionSet.Demand succeeded");
            ps.Assert();
            Console.WriteLine("PermissionSet.Assert succeeded");
            try
            {
                ps.PermitOnly();
                Console.WriteLine("PermissionSet.PermitOnly succeeded");
            }
            catch (PlatformNotSupportedException)
            {
                ColoredWriteLine("Caught platform not supported exception", ConsoleColor.Yellow);
            }

            Console.WriteLine();
        }

        private static void UseTransparency()
        {
            ColoredWriteLine("Testing transparency", ConsoleColor.Cyan);
            Console.WriteLine("Calling critical method");
            try
            {
                CriticalMethod();
            }
            catch (MethodAccessException)
            {
                ColoredWriteLine("Caught expected exception for going from Transparent -> Critical code", ConsoleColor.Yellow);
            }
            SafeCriticalMethod();
            Console.WriteLine();
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


        private static void ColoredWriteLine(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
