using AMXWrapperCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP.Shared.Scripting;

namespace TP.Shared.Scripting
{
    internal static partial class Natives
    {
        public static int GetBotUserName(AMX amx1, AMXArgumentList args1, Script caller_script)
        {
            if (args1.Length != 1) return 0;
            try
            {
                //Debug.WriteLine(caller_script.m_Instance.m_Bot.m_User.Username);
                AMX.SetString(args1[0].AsCellPtr(), caller_script.m_Instance.m_Bot.m_User.Username, true);
            }
            catch (Exception ex)
            {
                Utils.Log.Exception(ex, caller_script);
                Utils.Log.Error("In native 'GetBotUserName' (dest_string must be an char array, or received invalid parameters!)" + caller_script);
            }
            return 1;
        }
    }
}
