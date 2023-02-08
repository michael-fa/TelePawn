using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.Shared.Utils
{
    public static partial class Tools
    {
        public static string ReverseString(string s)
        {
            string result = String.Empty;
            char[] cArr = s.ToCharArray();
            int end = cArr.Length - 1;

            for (int i = end; i >= 0; i--)
            {
                result += cArr[i];
            }
            return result;
        }
    }
}
