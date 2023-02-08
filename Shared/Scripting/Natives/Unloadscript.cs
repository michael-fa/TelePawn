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
        public static int Unloadscript(AMX amx1, AMXArgumentList args1, Script caller_script)
        {
            if (args1.Length != 1) return 1;
            if (args1[0].AsString().Length == 0)
            {
                Utils.Log.Error("[Script] Error unloading new script! You did not specify a correct script file!");
                return 0;
            }

            foreach (Script sc in caller_script.m_Instance.m_Scripts)
            {
                if (sc.m_amxFile.Equals(args1[0].AsString()))
                {
                    AMXPublic pub = sc.m_Amx.FindPublic("OnUnload");
                    if (pub != null) pub.Execute();
                    sc.m_Amx.Dispose();
                    sc.m_Amx = null;
                    caller_script.m_Instance.m_Scripts.Remove(sc);
                    Utils.Log.Info("[Script] " + args1[0].AsString() + ".amx unloaded.");
                    return 1;
                }
            }
            Utils.Log.Error("[Script] " + args1[0].AsString() + ".amx is not running.");
            return 1;
        }
    }
}
