#define DEBUG
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace Dashboard.WebHost
{
    public static class ConsoleManager
    {
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", CharSet = CharSet.Unicode)]
        private static extern bool AllocConsole();
        public static void InitializeConsoleManager()
        {

#if (DEBUG)
            try      
                    {        
                        AllocConsole();        
                        Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput(), Encoding.Default, 100));
                        Console.Clear();
                    }      
                    catch (Exception)      
                    {
                    
                    }
        #endif
        }
    }
}