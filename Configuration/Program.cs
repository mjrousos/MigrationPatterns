using CustomConfiguration;
using System;
using System.Configuration;
using System.Diagnostics;

namespace Configuration
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Getting app settings");
            Console.WriteLine($"AppSetting value: {ConfigurationManager.AppSettings["MySetting"]}");
            Console.WriteLine();

            Console.WriteLine("Using System.Diagnostics configuration");
            var traceSource = new TraceSource("MySource");
#if NETCOREAPP
            ConfigureTraceSource(traceSource);
#endif
            traceSource.TraceInformation("Test message");
            Console.WriteLine();

            Console.WriteLine("Loading custom config section");
            var section = ConfigurationManager.GetSection("customSection") as CustomSection;
            Console.WriteLine($"Custom element: {section.Element.Id} - {section.Element.Name}");
            Console.WriteLine();

            Console.WriteLine("- Done -");
        }

#if NETCOREAPP
        // Diagnostics settings aren't automatically loaded from configuration
        // so configuration needs to be done in code (possibly based on our own
        // config settings).
        private static void ConfigureTraceSource(TraceSource traceSource)
        {
            traceSource.Listeners.Add(new ConsoleTraceListener());
            traceSource.Switch.Level = SourceLevels.Information;
        }
#endif
    }
}