using AMXWrapperCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TP.Shared.Scripting
{
    public class Script
    {
        public TelePawn m_Instance;
        public string m_amxFile = null!;
        public AMX m_Amx = null!;
        public bool m_isFs;


        public Script(TelePawn _bot, string _amxFile, bool _isFilterscript = false)
        {
            this.m_Instance = _bot!; 
            this.m_amxFile = _amxFile!;
            try
            {
                //Todo
                //_amxFile does include the whole path, even tho we hardcoded the scripts to be inside the /Scripts/ folder anyways.
                //maybe put the path below in the AMX initialiasion so that we skip the the        CurrentDirectory + "/Scripts/    part anywhere else.
                m_Amx = new AMX(_amxFile + ".amx");

            }
            catch (Exception e)
            {
                Utils.Log.Exception(e);
                return;
            }

            this.m_Amx.LoadLibrary(AMXDefaultLibrary.Core | AMXDefaultLibrary.Float | AMXDefaultLibrary.String | AMXDefaultLibrary.Console | AMXDefaultLibrary.DGram | AMXDefaultLibrary.Time);
            this.RegisterNatives();

            if (!_isFilterscript)
            {
                m_Instance.m_MainAMX = m_Amx;

                Utils.Log.Debug("[Script] Loading main.amx as ID: 1", this);
                this.m_Amx.ExecuteMain();
            }


            m_Instance.m_Scripts.Add(this);
            return;
        }

        public void _FSInit()
        {
            Utils.Log.Debug("[Script] Calling filterscript as ID: " + m_Instance.m_Scripts.Count, this);
            AMXPublic p = this.m_Amx.FindPublic("OnFilterscriptInit");
            if (p != null) p.Execute();
        }

        public bool RegisterNatives()
        {
            //Added core functionality
            m_Amx.Register("Loadscript", (amx1, args1) => Natives.Loadscript(amx1, args1, this));
            m_Amx.Register("Unloadscript", (amx1, args1) => Natives.Unloadscript(amx1, args1, this));
            m_Amx.Register("CallRemoteFunction", (amx1, args1) => Natives.CallRemoteFunction(amx1, args1, this));

            //Message
            m_Amx.Register("SendChatMessage", (amx1, args1) => Natives.SendChatMessage(amx1, args1, this));
            m_Amx.Register("DeleteChatMessage", (amx1, args1) => Natives.DeleteChatMessage(amx1, args1, this));
            m_Amx.Register("EditChatMessage", (amx1, args1) => Natives.EditChatMessage(amx1, args1, this));
            m_Amx.Register("PinChatMessage", (amx1, args1) => Natives.PinChatMessage(amx1, args1, this));
            m_Amx.Register("UnpinChatMessage", (amx1, args1) => Natives.UnpinChatMessage(amx1, args1, this));

            //Chat
            m_Amx.Register("GetChatType", (amx1, args1) => Natives.GetChatType(amx1, args1, this));
            m_Amx.Register("GetChatSlowModeDelay", (amx1, args1) => Natives.GetChatSlowModeDelay(amx1, args1, this));
            m_Amx.Register("GetChatDescription", (amx1, args1) => Natives.GetChatDescription(amx1, args1, this));
            m_Amx.Register("UnpinAllChatMessages", (amx1, args1) => Natives.UnpinAllChatMessages(amx1, args1, this));

            //User
            m_Amx.Register("GetUserName", (amx1, args1) => Natives.GetUserName(amx1, args1, this));
            m_Amx.Register("GetUserLegalName", (amx1, args1) => Natives.GetUserLegalName(amx1, args1, this));
            m_Amx.Register("GetUserBio", (amx1, args1) => Natives.GetUserBio(amx1, args1, this));

            //Bot
            m_Amx.Register("GetBotUsername", (amx1, args1) => Natives.GetBotUserName(amx1, args1, this));
            m_Amx.Register("GetBotName", (amx1, args1) => Natives.GetBotName(amx1, args1, this));
            return true;
        }
    }
}
