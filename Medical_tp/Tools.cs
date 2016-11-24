using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_tp
{
    class Tools
    {
        public static string tabStringToString(string[] tabString)
        {
            string str = "";

            foreach (string stmp in tabString)
                str += stmp + System.Environment.NewLine;


            return str;
        }

        public static string[] stringToTabString(string str)
        {
            string backLine = System.Environment.NewLine;
            return str.Split(backLine.ToCharArray());
        }
    }
}
