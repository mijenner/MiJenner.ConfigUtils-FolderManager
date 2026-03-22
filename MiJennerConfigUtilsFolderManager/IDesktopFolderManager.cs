namespace MiJenner.ConfigUtils
{
    public interface IDesktopFolderManager
    {
        public Platform Platform { get; }
        public string UserDataFolder { get; }
        public string UserConfigFolder { get; }

        bool TryGetDataFolderPath(out string folder);

        bool TryGetConfigFolderPath(out string folder);

        bool DataFolderExists();

        bool ConfigFolderExists();

        bool TryCreateUserDataFolder();

        bool TryCreateUserConfigFolder();
        bool TryCreateUserDataMagicFile();
        bool TryCreateUserConfigMagicFile();

        void UpdateConfiguration(DesktopFolderManagerConfig config);
    }
}
