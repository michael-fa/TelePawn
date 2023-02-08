using System.Runtime.InteropServices;
using System.Text;
using AMXWrapperCore;
using TP.Shared.Scripting;
using TP.Shared.Utils;

namespace TP.Shared
{
    public class TelePawn
    {
        //Vars, Objects
        public bool IsRunning                   = false;
        internal Telegram.Bot m_Bot;
        private TPConfiguration m_Config;
        internal AMXWrapperCore.AMX m_MainAMX   = null!;

        //Lists
        internal List<Scripting.Script> m_Scripts = null!;


        public TelePawn(TPConfiguration config)
        {
            Utils.Log.Debug("Creating TelePawn instance");
            CheckStuff(config);

            //Initialising stuff
            m_Scripts = new List<Scripting.Script>();


            this.m_Config   = config;
            this.m_Bot      = new Telegram.Bot(this, m_Config.Token);

            m_Bot.Listen();

            try
            {
                foreach (string fl in Directory.GetFiles(AppContext.BaseDirectory + @"/Scripts/"))
                {
                    //skipping files with ! just as a quick disabling method, and whatever file is not amxy.
                    if (fl.StartsWith("!") || fl.Contains("main.amx") || !fl.EndsWith(".amx")) continue;
                    Log.Info("[Script] Found filterscript: '" + fl + "' !");
                    new Script(this, fl.Replace(".amx", ""), true)._FSInit();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                StopSafely();
                return;
            }

            //Init main amx
            var x = new Script(this, AppContext.BaseDirectory + @"/Scripts/" + config.FirstScript);


            Utils.Log.Debug("..instance has been created.");
        }

        internal void CheckStuff(TPConfiguration config)
        {
            Utils.Log.Debug("..checking stuff to initialise instance.");

            //Check if Plugins dir exists
            if (!Directory.Exists(AppContext.BaseDirectory + "Plugins/"))
                Directory.CreateDirectory(AppContext.BaseDirectory + "Plugins/");

            //Check if Scripts dir exists
            if (!Directory.Exists(AppContext.BaseDirectory + "Scripts/"))
                Directory.CreateDirectory(AppContext.BaseDirectory + "Scripts/");

            //Is main amx there?
            if(!File.Exists(AppContext.BaseDirectory + @"/Scripts/" + config.FirstScript + ".amx"))
            {
                Log.Error("[Script] \"" + config.FirstScript + ".amx\" not found. Please make sure you have a valid script inside Scripts folder.");
                StopSafely();
            }


            //Load config.ini -> load plugins -> load telegram -> load scripting

            if (!Utils.ConfigFile.Load())
            {
                Utils.Log.Error(".. Error! Program exited. Reason: Invalid configuration.");
                Environment.Exit(0);
                return;
            }
        }
        public void StopSafely()
        {
            if (m_Scripts == null) goto skip1; //Must stay, we close the program using this before we init the list.
            foreach (Script script in m_Scripts)
            {
                if (script.m_Amx == null) continue;

                //script.StopAllTimers();

                if (script.m_Amx.FindPublic("OnUnload") != null)
                    script.m_Amx.FindPublic("OnUnload").Execute();

                script.m_Amx.Dispose();
                script.m_Amx = null!;
                Log.WriteLine("[Script] " + script.m_amxFile + " unloaded.");
            }

            skip1:

            if (m_Bot != null) m_Bot.Stop();

            //copy current log txt to one with the date in name and delete the old one | we also replace : or / to - so that theres no language based error in folder/file names
            File.Copy(System.AppContext.BaseDirectory + "/Logs/current.txt", (System.AppContext.BaseDirectory + "Logs/" + DateTime.Now.ToString().Replace(':', '-').Replace('/', '-') + ".txt"));
            if (File.Exists(System.AppContext.BaseDirectory + "/Logs/current.txt")) File.Delete(System.AppContext.BaseDirectory + "/Logs/current.txt");
            Environment.Exit(0);
        }
    }

    
}