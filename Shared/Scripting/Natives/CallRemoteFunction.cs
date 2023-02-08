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
        public static int CallRemoteFunction(AMX amx1, AMXArgumentList args1, Script caller_script)
        {
            try
            {
                if (args1.Length == 1)
                {
                    if (args1[0].AsString().Length < 2)
                        return 0;

                    AMXPublic tmp = null!;
                    foreach (Script scr in caller_script.m_Instance.m_Scripts)
                    {
                        tmp = scr.m_Amx.FindPublic(args1[0].AsString());
                        if (tmp != null) tmp.Execute();
                    }
                    return 1;
                }
                else if (args1.Length >= 3)
                {
                    int count = (args1.Length - 1);

                    AMXPublic p = null!;
                    List<CellPtr> Cells = new List<CellPtr>();

                    //Important so the format ( ex "iissii" ) is aligned with the arguments pushed to the callback, not being reversed
                    string reversed_format = Utils.Tools.ReverseString(args1[1].AsString());
                    
                    foreach (Script scr in caller_script.m_Instance.m_Scripts)
                    {
                        if (scr.Equals(caller_script)) continue;
                        p = scr.m_Amx.FindPublic(args1[0].AsString());
                        if (p == null) continue;
                        foreach (char x in reversed_format.ToCharArray())
                        {
                            if (count == 1) break;
                            switch (x)
                            {
                                case 'i':
                                    {
                                        p.AMX.Push(args1[count].AsInt32());
                                        count--;
                                        continue;
                                    }
                                case 'f':
                                    {
                                        p.AMX.Push((float)args1[count].AsCellPtr().Get().AsFloat());
                                        count--;
                                        continue;
                                    }

                                case 's':
                                    {
                                        Cells.Add(p.AMX.Push(args1[count].AsString()));
                                        count--;
                                        continue;
                                    }
                            }
                        }
                        //Reset our arg index counter
                        count = (args1.Length - 1);
                        p.Execute();

                    }

                    foreach (CellPtr cell in Cells)
                    {
                        p.AMX.Release(cell);
                    }
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                Utils.Log.Exception(ex);
            }
            return 1;
        }
    }
}
