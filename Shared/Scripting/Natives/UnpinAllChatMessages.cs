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
        public static int UnpinAllChatMessages(AMX amx1, AMXArgumentList args1, Script caller_script)
        {
            if (args1.Length != 1) return 0;
            Task.Run(async () =>
            {
                await caller_script.m_Instance.m_Bot.m_Client.UnpinAllChatMessages(Convert.ToString(args1[0].AsString())).ConfigureAwait(false);
            });
            return 1;
        }
    }
}
