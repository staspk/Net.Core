using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kozubenko.Utils
{
    /// <summary>
    /// Not sure if I need to bring over Kozubenko.Env.py into the .net clr. Will finish when the need actually arises.
    /// </summary>
    public class Env
    {
        public static Dictionary<string, string> Vars = new Dictionary<string, string>();

        public static string PathToEnvFile = "";

        private static string DefaultEnvAbsPath                               // Assumption: .env file resides on Solution level, will only work during development
        {
            get
            {
                var splitStr = Directory.GetCurrentDirectory().Split('\\');   // C:\Users\stasp\Desktop\C#\Kozubenko\Kozubenko\bin\Debug\net9.0
                string path = "";
                for (int i = 0; i < splitStr.Length - 4; i++)                 // 3 levels up
                {
                    path += $"{splitStr[i]}\\";
                }

                string absPath = $"{path}.env";                               // C:\Users\stasp\Desktop\C#\Kozubenko\.env

                if (System.IO.File.Exists(absPath))
                    return absPath;
                else
                    throw new Exception($".env file not found at {absPath}");
            }
        }
    }
}
