using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WeakImages
{
    internal class LogSys
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        internal static void BeginConsole()
        {
            AllocConsole();
        }
        internal static void InfoLog(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("[INFO]" + message);
            Console.ResetColor();
        }
        internal static void ErrorLog(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERR]" + message);
            Console.ResetColor();
        }

    }
}
