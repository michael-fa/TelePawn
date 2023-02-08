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
        public static int GetChatSlowModeDelay(AMX amx1, AMXArgumentList args1, Script caller_script)
        {
            if (args1.Length != 1) return 0;
            try
            {
                var res = Task.Run(async () =>
                {
                    Chat _Chat = await caller_script.m_Instance.m_Bot.m_Client.GetChatAsync(args1[0].AsString()).ConfigureAwait(false);
                    return _Chat;
                });
                return Convert.ToInt32(res.Result.SlowModeDelay);
            }
            catch (Exception ex)
            {
                Utils.Log.Exception(ex, caller_script);
                Utils.Log.Error("In native 'GetChatSlowModeDelay' (invalid chat id, or not a supergroup)" + caller_script);
            }
            return 1;
        }
    }
}
