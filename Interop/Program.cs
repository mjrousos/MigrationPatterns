using Interop.IjwLibrary;
using System;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace Interop
{
    class Program
    {
        static void Main()
        {
            CppCliInterop();
            COMReference();
            DynamicCOMInterop();
        }

        private static void COMReference()
        {
            var excel = new Excel.Application();
            excel.Visible = true;
            excel.UserControl = true;
        }

        private static void CppCliInterop() =>
            IjwClass.Greet();

        private static void DynamicCOMInterop()
        {
            var progId = "InternetExplorer.Application";
            var type = Type.GetTypeFromProgID(progId);
            dynamic inst = Activator.CreateInstance(type);
#if NET472
            inst.Visible = true;
            inst.Navigate("https://www.microsoft.com");
#elif NETCOREAPP
            type.InvokeMember("Visible", BindingFlags.SetProperty, Type.DefaultBinder, inst, new object[] { true });
            type.InvokeMember("Navigate", BindingFlags.InvokeMethod, Type.DefaultBinder, inst, new object[] { "https://www.microsoft.com" });
#endif
        }
    }
}
