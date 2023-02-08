using AMXWrapperCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TP.Shared.Scripting
{
    internal static partial class Events
    {
        public static void Message(Telegram.Bot bot, Update update)
        {
            AMXPublic p;
            foreach (Scripting.Script scr in bot.m_TPHost.m_Scripts)
            {
                p = scr.m_Amx.FindPublic("OnMessage");
                if (p != null)
                {
                    if (update.Message.From == null)
                        continue;

                    var tmp = p.AMX.Push(update.Message.Text);
                    var tmp2 = p.AMX.Push(update.Message.From.Id.ToString());
                    p.AMX.Push(update.Message.MessageId);
                    var tmp4 = p.AMX.Push(update.Message.Chat.Id.ToString());

                    p.Execute();
                    p.AMX.Release(tmp);
                    p.AMX.Release(tmp2);
                    p.AMX.Release(tmp4);

                    GC.Collect();
                }
            }
        }
    }
}
