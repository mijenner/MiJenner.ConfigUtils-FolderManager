using MiJenner.ConfigUtils;

namespace UsageExamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new DesktopFolderManagerConfigBuilder()
                .WithUserDataPolicy(UserDataPolicy.PolicyFileDocument)
                .WithUserDataMagic("")
                .WithUserConfigPolicy(UserConfigPolicy.PolicyFileDocument)
                .WithUserConfigMagic("")
                .WithCompanyAndAppName("MiJenner", "LearningApp")
                .Build();

            var folderManager = new DesktopFolderManager(config);

            folderManager.TryGetConfigFolderPath(out string configFolder);
            folderManager.TryGetDataFolderPath(out string dataFolder);
            Console.WriteLine($"Config folder : {configFolder}");
            Console.WriteLine($"Data folder   : {dataFolder}");

            folderManager.TryCreateUserConfigFolder();
            folderManager.TryCreateUserDataFolder();
            Console.WriteLine($"Config exists : {Directory.Exists(configFolder)}");
            Console.WriteLine($"Data exists   : {Directory.Exists(dataFolder)}");

            // Test skrivning til config-mappe
            Console.WriteLine($"Config write  : {TryWriteTestFile(configFolder)}");

            // Test skrivning til data-mappe
            Console.WriteLine($"Data write    : {TryWriteTestFile(dataFolder)}");
        }

        private static bool TryWriteTestFile(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder)) return false;
            string testFile = Path.Combine(folder, "_writetest.tmp");
            try
            {
                File.WriteAllText(testFile, "write test");
                // File.Delete(testFile);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
