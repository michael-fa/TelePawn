using AMXWrapperCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TP.Shared.Scripting
{
    internal static partial class Natives
    {
        public static int GetChatType(AMX amx1, AMXArgumentList args1, Script caller_script)
        {
            if (args1.Length != 1) return 0;
            try
            {
                var res = Task.Run(async () =>
                {
                    Chat _Chat = await caller_script.m_Instance.m_Bot.m_Client.GetChatAsync(args1[0].AsString()).ConfigureAwait(false);
                    return _Chat;
                });

                switch (res.Result.Type)
                {
                    case ChatType.Private:
                        return 0;
                    case ChatType.Group:
                        return 1;
                    case ChatType.Channel:
                        return 2;
                    case ChatType.Sender:
                        return 3;
                    case ChatType.Supergroup:
                        return 4;
                }
            }
            catch (Exception ex)
            {
                Utils.Log.Exception(ex, caller_script);
                Utils.Log.Error("In native 'GetChatType' (invalid chatid?)" + caller_script);
            }
            return -1;
        }
    }
}
