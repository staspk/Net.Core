using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kozubenko.IO
{
    public class File
    {
        /// <summary>
        ///     Generally used to store user preferences, application state info, logs
        /// </summary>
        /// <param name="ApplicationName"></param>
        /// <returns>
        ///     Windows:                C:\Users\{user}\AppData\Roaming\{ApplicationName}\
        ///     Unix/Linux:             home/{user}/.{ApplicationName}/
        ///     Exception Fallback:     {path/to/executable}/.app_data/
        /// </returns>
        public static string ApplicationDataDirectory(string ApplicationName)
        {
            try
            {
                string roaming = OperatingSystem.IsWindows() ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                                                             : Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                string appDataDirectory = OperatingSystem.IsWindows() ? Path.Combine(roaming, ApplicationName)
                                                                      : Path.Combine(roaming, $".{ApplicationName}");

                if (!Directory.Exists(appDataDirectory))
                    Directory.CreateDirectory(appDataDirectory);

                return appDataDirectory;
            }
            catch(Exception e)
            {

                string executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString();

                string fallbackDataDirectory = Path.Combine(executableDirectory, ".app_data");

                if (!Directory.Exists(fallbackDataDirectory))
                    Directory.CreateDirectory(fallbackDataDirectory);

                return fallbackDataDirectory;
            }
        }

        /// <summary>
        ///     See ApplicationDataDirectory()
        /// </summary>
        public static string GenerateConfigFile(string ApplicationName, string fileName = "log.txt")
        {
            string appDataDirectory = ApplicationDataDirectory(ApplicationName);

            if (!System.IO.File.Exists(appDataDirectory))
                System.IO.File.Create(appDataDirectory);

            return appDataDirectory;
        }

    }
}