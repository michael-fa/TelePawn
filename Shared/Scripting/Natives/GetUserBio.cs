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
        public static int GetUserBio(AMX amx1, AMXArgumentList args1, Script caller_script)
        {
            if (args1.Length != 2) return 0;
            try
            {
                var res = Task.Run(async () =>
                {
                    Chat _Chat = await caller_script.m_Instance.m_Bot.m_Client.GetChatAsync(args1[0].AsString()).ConfigureAwait(false);
                    return _Chat.Bio;
                });
                if(res.Result == null) return 0;
                AMX.SetString(args1[1].AsCellPtr(), res.Result, true);
            }
            catch (Exception ex)
            {
                Utils.Log.Exception(ex, caller_script);
                Utils.Log.Error("In native 'GetUserBio' (dest_string must be an char array, invalid parameters, or you try to get bio from an group chat)" + caller_script);
            }
            return 1;
        }
    }
}
