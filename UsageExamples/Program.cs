using MiJenner.ConfigUtils;

namespace UsageExamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new DesktopFolderManagerConfigBuilder()
                // .WithUserDataPolicy(UserDataPolicy.PolicyFileDocument)
                .WithUserDataMagic("")
                // .WithUserConfigPolicy(UserConfigPolicy.PolicyFileDocument)
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

            


        }
    }
}
