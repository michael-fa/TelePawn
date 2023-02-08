using AMXWrapperCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TP.Shared.Telegram
{
    internal class Bot
    {
        internal TelePawn m_TPHost = null!;
        internal User m_User = null!;
        internal TelegramBotClient m_Client;
        private CancellationTokenSource m_Cts;
        private CancellationToken m_CancellationToken;
        private ReceiverOptions m_rcvOptions;

        public Bot(TelePawn Host, string Token)
        { 
            m_TPHost = Host;
            try
            {
                m_Client = new TelegramBotClient(Token);
                m_User = m_Client.GetMeAsync().Result;
            }
            catch ( Exception ex)
            {
                Utils.Log.Exception(ex);
                Utils.Log.Info("Above warning may be caused by an invalid token!");
                Host.StopSafely();
            }

            

            m_Cts = new CancellationTokenSource();
            m_CancellationToken = m_Cts.Token;
            m_rcvOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };
        }

        public bool Listen()
        {
            if (m_TPHost.IsRunning == true) return false;
            this.m_Client.StartReceiving(
               this.HandleUpdateAsync,
               this.HandleErrorAsync,
               m_rcvOptions,
               m_CancellationToken
           );
            m_TPHost.IsRunning = true;
            return true;
        }

        public void Stop()
        {
            m_Client.CloseAsync();
            this.m_TPHost.IsRunning = false;
        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (!m_TPHost.IsRunning) return;

            await Task.Run(() => {
                var message = update.Message;
                if (update.Message != null && update.EditedMessage == null)
                {
                    Utils.Log.Debug("[RAW] Telegram Update (" + update.Message.Type + ") received.. ");
                    AMXPublic p;
                    switch (message.Type)
                    {
                        case MessageType.Text:
                            Scripting.Events.Message(this, update);
                            break;

                        case MessageType.ChatMembersAdded:
                            foreach (User user in message.NewChatMembers)
                            {
                                foreach (Scripting.Script scr in m_TPHost.m_Scripts)
                                {
                                    p = scr.m_Amx.FindPublic("OnChatMemberAdded");

                                    if (p == null || message.From == null) continue;

                                    var tmp = p.AMX.Push(user.Id.ToString());
                                    var tmp3 = p.AMX.Push(message.Chat.Id.ToString());
                                    p.Execute();
                                    p.AMX.Release(tmp);
                                    p.AMX.Release(tmp3);
                                }
                            }
                            break;

                        case MessageType.ChatMemberLeft:
                            foreach (Scripting.Script scr in m_TPHost.m_Scripts)
                            {
                                p = scr.m_Amx.FindPublic("OnChatMemberLeft");

                                if (p == null || message.From == null) continue;

                                var tmp = p.AMX.Push(message.LeftChatMember.Id.ToString());
                                var tmp3 = p.AMX.Push(message.Chat.Id.ToString());
                                p.Execute();
                                p.AMX.Release(tmp);
                                p.AMX.Release(tmp3);
                            }
                            break;
                    }
                }
                else if(update.EditedMessage != null)
                {
                    Utils.Log.Debug("[RAW] Telegram MESSAGE EDITED Update (" + update.EditedMessage.Type + ") received.. ");
                    Scripting.Events.EditedMessage(this, update);
                }
                else Utils.Log.Debug("Unhandled telegram update received ( " + update.Type.ToString() + " ).");
            });
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            //Untested but should work somehow.
            if (exception is ApiRequestException apiRequestException)
            {

                await Task.Run(() =>
                {
                    Utils.Log.Debug("Telegram exception " + apiRequestException.ToString() + ": " + apiRequestException.Message + "  at  " + apiRequestException.Source);

                    AMXPublic p;
                    foreach (Scripting.Script scr in m_TPHost.m_Scripts)
                    {
                        p = scr.m_Amx.FindPublic("OnTelegramError");

                        if (p != null)
                        {
                            var tmp1 = p.AMX.Push(apiRequestException.Message);
                            var tmp2 = p.AMX.Push(apiRequestException.Source);
                            p.AMX.Push(apiRequestException.ErrorCode);
                            p.Execute();
                            p.AMX.Release(tmp1);
                            p.AMX.Release(tmp2);
                        }
                    }


                });
            }
        }


    }
}
