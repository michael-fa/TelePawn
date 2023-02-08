using System.Diagnostics;

namespace TP.Shared
{
    public struct TPConfiguration
    {
        private string m_Token;
        private string m_MainScript;

        public string Token
        {
            get { return m_Token; }
        }

        public string FirstScript
        {
            get { return m_MainScript; }
        }

        public TPConfiguration(string token, string mainscript)
        {
            m_Token = token;
            m_MainScript = mainscript;
            Utils.Log.Debug("Created TPConfiguration { Token: " + m_Token.Remove(10) + ".." + m_Token.Substring(20) + " | MainScr: " + m_MainScript + "}");
        }
    }
}