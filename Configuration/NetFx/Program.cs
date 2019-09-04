using CustomConfiguration;
using System;
using System.Configuration;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Console.WriteLine("Getting app settings");
        Console.WriteLine($"AppSetting value: {ConfigurationManager.AppSettings["MySetting"]}");
        Console.WriteLine();

        Console.WriteLine("Using System.Diagnostics configuration");
        var traceSource = new TraceSource("MySource");
        traceSource.TraceInformation("Test message");
        Console.WriteLine();

        Console.WriteLine("Loading custom config section");
        var section = ConfigurationManager.GetSection("customSection") as CustomSection;
        Console.WriteLine($"Custom element: {section.Element.Id} - {section.Element.Name}");
        Console.WriteLine();

        Console.WriteLine("- Done -");
    }
}
