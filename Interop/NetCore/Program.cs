using System;

namespace Sample
{
    class Program
    {
        static void Main()
        {
            var progId = "InternetExplorer.Application";
            var type = Type.GetTypeFromProgID(progId);
            dynamic inst = Activator.CreateInstance(type);

            inst.Visible = true;
            inst.Navigate("https://www.microsoft.com");
        }
    }
}
