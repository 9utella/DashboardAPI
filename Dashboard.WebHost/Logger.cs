using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.WebHost
{
    public static class Logger
    {
        //[ThreadStatic]
        //private static Severity m_CurrentSeverity;

        //public static void WhriteToLog(string message)
        //{
        //    WhriteToLog(message, Severity.Debug);
        //}
        //public static void WriteToLog(string message, Severity severity)
        //{
        //    lock (typeof(Logger))
        //    {
        //        m_CurrentSeverity = severity;
        //        WriteLineStart();
        //        WriteLine(message);
        //    }
        //}
        //private static void WriteLine(string message) { Write(message + Environment.NewLine); }
        //private static void Write(string message, params object[] parameters)
        //{
        //    Write(string.Format(message, parameters));
        //}
        //private static void Write(string message)
        //{
        //    Console.Write(message); if (m_CurrentSeverity == Severity.Error || m_CurrentSeverity == Severity.Event);
        //}
    }
}