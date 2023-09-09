namespace MiJenner.ConfigUtils
{
    public class DesktopFolderManagerConfigBuilder
    {
        private readonly DesktopFolderManagerConfig config = new DesktopFolderManagerConfig();

        public DesktopFolderManagerConfigBuilder WithUserDataPolicy(UserDataPolicy policy)
        {
            config.UserDataPolicy = policy;
            return this;
        }

        public DesktopFolderManagerConfigBuilder WithUserDataMagic(string magic)
        {
            config.UserDataMagic = magic;
            return this;
        }

        public DesktopFolderManagerConfigBuilder WithUserConfigPolicy(UserConfigPolicy policy)
        {
            config.UserConfigPolicy = policy;
            return this;
        }

        public DesktopFolderManagerConfigBuilder WithUserConfigMagic(string magic)
        {
            config.UserConfigMagic = magic;
            return this;
        }

        public DesktopFolderManagerConfigBuilder WithCompanyAndAppName(string companyName, string appName)
        {
            config.CompanyName = companyName;
            config.AppName = appName;
            return this;
        }

        public DesktopFolderManagerConfig Build()
        {
            return config;
        }
    }
}
