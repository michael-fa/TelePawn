using AMXWrapperCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TP.Shared.Telegram;

namespace TP.Shared.Scripting
{
    internal static partial class Events
    {
        public static void EditedMessage(Telegram.Bot bot, Update update)
        {
            AMXPublic p;
            foreach (Scripting.Script scr in bot.m_TPHost.m_Scripts)
            {
                p = scr.m_Amx.FindPublic("OnMessageEdited");
                if (p != null)
                {
                    if (update.EditedMessage.From == null)
                        continue;

                    var tmp = p.AMX.Push(update.EditedMessage.Text);
                    var tmp2 = p.AMX.Push(update.EditedMessage.From.Id.ToString());
                    p.AMX.Push(update.EditedMessage.MessageId);
                    var tmp3 = p.AMX.Push(update.EditedMessage.Chat.Id.ToString());

                    p.Execute();
                    p.AMX.Release(tmp);
                    p.AMX.Release(tmp2);
                    p.AMX.Release(tmp3);
                }
            }
        }
    }
}
