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
        ///     On Windows, creates, if path does not exist, and returns:                 C:\Users\{user}\AppData\Roaming\{ApplicationName}\{configFileName}
        ///     On Unix / Linux, creates, if path does not exist, and returns:            home/{user}/{ApplicationName}/{configFileName}
        ///     On Exception, will fallback to, create if does not exist, and returns:    {path/to/executable}/{configFileName}
        /// </summary>
        /// <param name="ApplicationName"></param>
        /// <param name="configFileName"></param>
        /// <returns></returns>
        public static string GenerateConfigFilePath(string ApplicationName, string configFileName = "db.sqlite3")
        {
            try
            {
                string roaming = OperatingSystem.IsWindows() ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                                                             : Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                string appDataDirectory = Path.Combine(roaming, ApplicationName);

                if (!Directory.Exists(appDataDirectory))
                    Directory.CreateDirectory(appDataDirectory);

                return Path.Combine(appDataDirectory, configFileName);
            }
            catch
            {
                string executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString();

                string fallbackDataDirectory = Path.Combine(executableDirectory, ".app_data");

                if (!Directory.Exists(fallbackDataDirectory))
                    Directory.CreateDirectory(fallbackDataDirectory);

                return Path.Combine(fallbackDataDirectory, configFileName);
            }
        }

        /// <summary>
        ///     Behavior identical to GenerateConfigFile, but does not create configFile if it does not exist.
        ///     It's parent directory will still be created, if it does not exist.
        /// </summary>
        public static string GenerateConfigFile(string ApplicationName, string configFileName = "log.txt")
        {
            string appDataDirectory = GenerateConfigFilePath(ApplicationName, configFileName);

            if (!System.IO.File.Exists(appDataDirectory))
                System.IO.File.Create(appDataDirectory);

            return appDataDirectory;
        }
    }
}