using MiJenner.ConfigUtils;

namespace UsageExamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new DesktopFolderManagerConfigBuilder()
                .WithUserDataPolicy(UserDataPolicy.PolicyFileAppDataRoaming)
                .WithUserDataMagic("")
                .WithUserConfigPolicy(UserConfigPolicy.PolicyFileAppDataRoaming)
                .WithUserConfigMagic("")
                .WithCompanyAndAppName("YourCompany", "YourApp")
                .Build();

            var folderManager = new DesktopFolderManager(config);

            string configFolder;
            string dataFolder; 

            folderManager.TryGetConfigFolderPath(out configFolder);
            folderManager.TryGetDataFolderPath(out dataFolder);

            Console.WriteLine(configFolder);
            Console.WriteLine(dataFolder);

            folderManager.TryCreateUserConfigFolder();
            folderManager.TryCreateUserDataFolder(); 

            Console.WriteLine(Directory.Exists(configFolder));
            Console.WriteLine(Directory.Exists(dataFolder));

           

        }
    }
}
