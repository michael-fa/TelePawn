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
        public static int SendChatMessage(AMX amx1, AMXArgumentList args1, Script caller_script)
        {
            if (args1.Length != 3) return 0;
            Task.Run(async () =>
            {
                Message msg = null;
                if (args1[2].AsInt32() != -1)
                {
                    msg = await caller_script.m_Instance.m_Bot.m_Client.SendTextMessageAsync((ChatId)args1[0].AsString(), args1[1].AsString(), null, null, null, null, null, null, args1[2].AsInt32());
                }
                else
                {
                    msg = await caller_script.m_Instance.m_Bot.m_Client.SendTextMessageAsync((ChatId)args1[0].AsString(), args1[1].AsString());
                }
                return msg.MessageId;
            });

            return 1;
        }
    }
}
