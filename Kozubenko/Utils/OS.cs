


namespace Kozubenko.Utils
{
    public enum OperatingSystemType
    {
        Windows,
        MacOS,
        Ubuntu,
        Linux,
        Unknown
    }

    public class OS
    {
        public static OperatingSystemType CheckPlatform()
        {
            if (OperatingSystem.IsWindows())
                return OperatingSystemType.Windows;

            return OperatingSystemType.Unknown;
        }
    }
}
