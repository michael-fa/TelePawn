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
        public static int GetUserLegalName(AMX amx1, AMXArgumentList args1, Script caller_script)
        {
            if (args1.Length != 3) return 0;
            try
            {
                var res = Task.Run(async () =>
                {
                    Chat _Chat = await caller_script.m_Instance.m_Bot.m_Client.GetChatAsync(args1[0].AsString()).ConfigureAwait(false);
                    return caller_script.m_Instance.m_Bot.m_Client.GetChatMemberAsync(_Chat.Id, Convert.ToInt64(args1[1].AsString())).Result.User.FirstName;
                });
                AMX.SetString(args1[2].AsCellPtr(), res.Result, true);
            }
            catch (Exception ex)
            {
                Utils.Log.Exception(ex, caller_script);
                Utils.Log.Error("In native 'GetUserLegalName' (dest_string must be an char array, user not found, or invalid parameters!)" + caller_script);
            }
            return 1;
        }
    }
}
