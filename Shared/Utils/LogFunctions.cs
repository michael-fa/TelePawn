using System;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;

namespace TP.Shared.Utils
{
    public static class Log
    {
        public static void WriteLine(string _msg)
        {
            Console.WriteLine(_msg);
            if (_msg.Length > 0) File.AppendAllText(@Environment.CurrentDirectory + "/Logs/current.txt", _msg + "\n");
        }
        public static void Info(string _msg, Scripting.Script _source = null)
        {
            if (_source != null)
            {
                string[] scrname = _source.m_amxFile.Split("/");
                Console.WriteLine("[INFO] [" + scrname[scrname.Length - 1] + "] " + _msg);
            }
            else Console.WriteLine("[INFO] " + _msg);

            if (_msg.Length > 0) File.AppendAllText(@Environment.CurrentDirectory + "/Logs/current.txt", _msg + "\n");
        }

        public static void Error(string _msg, Scripting.Script _source = null)
        {
            if (_source != null)
            {
                string[] scrname = _source.m_amxFile.Split("/");
                Console.WriteLine("[ERROR] [" + scrname[scrname.Length - 1] + "] " + _msg);
            }
            else Console.WriteLine("[ERROR] " + _msg);
            if (_msg.Length > 0) File.AppendAllText(@Environment.CurrentDirectory + "/Logs/current.txt", _msg + "\n");
        }

        public static void Warning(string _msg, Scripting.Script _source = null)
        {
            if (_source != null) 
            {
                string[] scrname = _source.m_amxFile.Split("/");
                Console.WriteLine("[WARNING] [" + scrname[scrname.Length - 1] + "] " + _msg);
            }
            else Console.WriteLine("[WARNING] " + _msg);
            if (_msg.Length > 0) File.AppendAllText(@Environment.CurrentDirectory + "/Logs/current.txt", _msg + "\n");
        }


        public static void Debug(string _msg, Scripting.Script _source = null)
        {
#if DEBUG
            if (_source != null)
            {
                string[] scrname = _source.m_amxFile.Split("/");
                Console.WriteLine("[DEBUG] [" + scrname[scrname.Length - 1] + "] " + _msg);
                System.Diagnostics.Debug.WriteLine("Utils.Log: <" + scrname[scrname.Length - 1] + "> " + _msg);
            }
            else
            {
                Console.WriteLine("[DEBUG] " + _msg);
                System.Diagnostics.Debug.WriteLine("Utils.Log: " + _msg);
            }
            if (_msg.Length > 0) File.AppendAllText("Logs/current.txt", _msg + "\n");


            
#endif
        }


        public static void Exception(Exception e)
        {
            Console.WriteLine("---------------------------------------\n[EXCEPTION] " + e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace + "\n---------------------------------------\n");
            File.AppendAllText(@Environment.CurrentDirectory + "/Logs/current.txt", "---------------------------------------\n[EXCEPTION] " + e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace + "\n-------------------------------------- -\n");
        }

        public static void Exception(@Exception e, Scripting.Script _source = null)
        {
            if (_source == null)
            {
                Console.WriteLine("---------------------------------------\n[EXCEPTION] " + e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace + "\n---------------------------------------\n");
                File.AppendAllText(@Environment.CurrentDirectory + "/Logs/current.txt", "---------------------------------------\n[EXCEPTION] " + e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace + "\n-------------------------------------- -\n");
            }
            else
            {
                Console.WriteLine("---------------------------------------\n[EXCEPTION] [" + _source.m_amxFile + "] " + e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace + "\n---------------------------------------\n");
                File.AppendAllText(@Environment.CurrentDirectory + "/Logs/current.txt", "---------------------------------------\n[EXCEPTION] [" + _source.m_amxFile + "] " + e.Message + "\n" + e.Source + "\n" + e.InnerException + "\n" + e.StackTrace + "\n-------------------------------------- -\n");

            }
        }
    }
}