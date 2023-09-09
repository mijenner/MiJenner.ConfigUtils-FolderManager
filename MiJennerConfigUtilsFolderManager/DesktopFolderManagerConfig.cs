namespace MiJenner.ConfigUtils
{
    public class DesktopFolderManagerConfig
    {
        public UserDataPolicy UserDataPolicy { get; set; } = UserDataPolicy.PolicyFileDocument;
        public string UserDataMagic { get; set; } = string.Empty;
        public UserConfigPolicy UserConfigPolicy { get; set; } = UserConfigPolicy.PolicyFileAppDataLocal;
        public string UserConfigMagic { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string AppName { get; set; } = string.Empty; 
    }
}

