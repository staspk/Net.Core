using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kozubenko.IO
{
    public class File
    {
        /// <summary>
        /// On Windows, creates, if path does not exist, and returns:                C:\Users\{user}\AppData\Roaming\{ApplicationName}\{configFileName}
        /// 
        /// On Unix/Linux, creates, if path does not exist, and returns:            /home/{username}/{ApplicationName}/{configFileName}
        /// 
        /// On exception, will fallback to, create if does not exist, and returns:  {path/to/assembly}/{configFileName}
        /// </summary>
        /// <param name="ApplicationName"></param>
        /// <param name="configFileName"></param>
        /// <returns></returns>
        public static string GenerateConfigFile(string ApplicationName, string configFileName = "log.txt")
        {
            try
            {
                string roaming = OperatingSystem.IsWindows() ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                                                             : Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                string roamingAppDataDirectory = Path.Combine(roaming, ApplicationName);

                if (!Directory.Exists(roamingAppDataDirectory))
                    Directory.CreateDirectory(roamingAppDataDirectory);

                string roamingConfigFile = Path.Combine(roaming, ApplicationName, configFileName);

                if (!System.IO.File.Exists(roamingConfigFile))
                    System.IO.File.Create(roamingConfigFile);

                return roamingConfigFile;
            }
            catch
            {
                string executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString();

                string fallbackConfigFile = Path.Combine(executableDirectory, configFileName);

                if(!System.IO.File.Exists(fallbackConfigFile))
                    System.IO.File.Create(fallbackConfigFile);

                return fallbackConfigFile;
            }
        }
    }
}
