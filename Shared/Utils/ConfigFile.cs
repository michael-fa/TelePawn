using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.Shared.Utils
{
    internal static class ConfigFile
    {
        internal struct ConfigData
        {
            public string m_Token;
        } static public ConfigData Config = new ConfigData();

        public static bool Load()
        {
            IniFile INI = null!;
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\config.ini"))
            {
                Log.Warning("Config.ini file not found and created. Please set your bot token.");
                INI = new IniFile(AppDomain.CurrentDomain.BaseDirectory + @"\config.ini");
            }
            else INI = new IniFile(AppDomain.CurrentDomain.BaseDirectory + @"\config.ini");

            if (INI == null) return false;
           
            
            if (!INI.KeyExists("token", "telegram"))
            {
                //Error, no bot token set
                Log.Error("No bot token has been set, please set a valid bot token in the config.ini");
                INI.Write("token", "changeme", "telegram");
                return false;
            }
            else
            {
                if(INI.Read("token", "telegram") == null)
                {
                    //Bot token not set
                    return false;
                }
                else Config.m_Token = INI.Read("token", "telegram");
            }


            return true;
        }
    }
}
