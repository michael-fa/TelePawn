using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.Shared.Utils
{
    /*
    internal static class ConsoleCommands
    {
        public async static void Loop()
        {
            string line = Console.ReadLine()!;
            string[] cmd = line.Split(' ');
            while (true)
            {
                if(line != null && cmd.Length > 0)
                {
                    switch(cmd[0])
                    {
                        case "exit":
                            Log.Info("[cmd] exit command was issued.");
                            Program.m_Bot.StopSafely();
                            break;
                    }
                }

                Thread.Sleep(1000);
                line = Console.ReadLine()!;
                cmd = line.Split(' ');

                await Task.Yield();
            }
        }
    }*/ 
}
