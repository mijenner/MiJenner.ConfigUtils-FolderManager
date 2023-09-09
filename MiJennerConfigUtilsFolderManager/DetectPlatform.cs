using System.Runtime.InteropServices;

namespace MiJenner.ConfigUtils
{
    public static class DetectPlatform
    {
        public static Platform TryDetect()
        {
            Platform platform = Platform.Unknown; 

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                platform = Platform.Windows;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                platform = Platform.MacOS;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                platform = Platform.Linux;
            }
            return platform; 
        }
    }
}
