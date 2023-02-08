using AMXWrapperCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TP.Shared.Scripting
{
    internal static partial class Natives
    {
        public static int Loadscript(AMX amx1, AMXArgumentList args1, Script caller_script)
        {
            if (args1.Length != 1) return 0;
            if (args1[0].AsString().Length == 0)
            {
                Utils.Log.Error("[Script] Error loading new script! You did not specify a correct script file!", caller_script);
                return 0;
            }

            if (!System.IO.File.Exists("Scripts/" + args1[0].AsString() + ".amx"))
            {
                Utils.Log.Error("[Script] Error loading new script! " + args1[0].AsString() + ".amx does not exist in /Scripts/ folder.", caller_script);
                return 0;
            }

            foreach (Script x in caller_script.m_Instance.m_Scripts)
            {
                //Todo
                //We should ACTUALLY check the whole file footprint and compare them.
                if (x.m_amxFile.Equals(args1[0].AsString())) //There is a better way, but still; we can always do or a unhandled error here.
                {
                    Utils.Log.Error("[Script] Error loading new script! " + args1[0].AsString() + ".amx is already loaded!");
                    return 0;
                }
            }

            new Script(caller_script.m_Instance, args1[0].AsString(), true)._FSInit();
            return 1;
        }
    }
}
