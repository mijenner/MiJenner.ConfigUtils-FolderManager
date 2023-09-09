namespace MiJenner.ConfigUtils
{
    public class DesktopFolderManagerConfig
    {
        public UserDataPolicy UserDataPolicy { get; set; } = UserDataPolicy.PolicyFileAppDataRoaming;
        public string UserDataMagic { get; set; } = string.Empty;
        public UserConfigPolicy UserConfigPolicy { get; set; } = UserConfigPolicy.PolicyFileAppDataRoaming;
        public string UserConfigMagic { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string AppName { get; set; } = string.Empty; 
    }
}

