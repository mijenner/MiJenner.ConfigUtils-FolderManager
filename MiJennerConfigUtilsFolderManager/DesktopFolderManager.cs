namespace MiJenner.ConfigUtils
{
    public class DesktopFolderManager : IDesktopFolderManager
    {
        private DesktopFolderManagerConfig config;
        private Platform platform;
        private string userDataFolder = string.Empty;
        private string userConfigFolder = string.Empty;
        private string magicDataFilePath = string.Empty;
        private string magicConfigFilePath = string.Empty;

        public Platform Platform
        {
            get => platform;
        }
        public string UserDataFolder
        {
            get => userDataFolder;
        }
        public string UserConfigFolder
        {
            get => userConfigFolder;
        }

        public DesktopFolderManager(DesktopFolderManagerConfig config)
        {
            // store config locally, 
            this.config = config;
            // try to determine paths: 
            TryDetermineDataFolderPath();
            TryDetermineConfigFolderPath();

            // determine platform, 
            platform = DetectPlatform.TryDetect();
        }

        public void UpdateConfiguration(DesktopFolderManagerConfig config)
        {
            this.config = config;

            // try to determine paths: 
            TryDetermineDataFolderPath();
            TryDetermineConfigFolderPath();

            // determine platform, 
            platform = DetectPlatform.TryDetect();
        }



        private bool TryDetermineDataFolderPath()
        {
            userDataFolder = string.Empty;
            switch (config.UserDataPolicy)
            {
                case UserDataPolicy.Unknown:
                    break;
                case UserDataPolicy.PolicyFileDocument:
                    userDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    break;
                case UserDataPolicy.PolicyFileAppDataLocal:
                    userDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    break;
                case UserDataPolicy.PolicyFileAppDataRoaming:
                    userDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrWhiteSpace(userDataFolder))
            {
                userDataFolder = AppendCompanyAppName(userDataFolder);
                if (!string.IsNullOrWhiteSpace(config.UserDataMagic))
                {
                    magicDataFilePath = Path.Combine(userDataFolder, (config.UserDataMagic + ".txt"));
                }
                else
                {
                    magicDataFilePath = string.Empty;
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        private bool TryDetermineConfigFolderPath()
        {
            userConfigFolder = string.Empty;
            switch (config.UserConfigPolicy)
            {
                case UserConfigPolicy.Unknown:
                    break;
                case UserConfigPolicy.PolicyFileDocument:
                    userConfigFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    break;
                case UserConfigPolicy.PolicyFileAppDataLocal:
                    userConfigFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    break;
                case UserConfigPolicy.PolicyFileAppDataRoaming:
                    userConfigFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrWhiteSpace(userConfigFolder))
            {
                userConfigFolder = AppendCompanyAppName(userConfigFolder);
                if (!string.IsNullOrWhiteSpace(config.UserConfigMagic))
                {
                    magicConfigFilePath = Path.Combine(userConfigFolder, (config.UserConfigMagic + ".txt"));
                }
                else
                {
                    magicConfigFilePath = string.Empty;
                }
                return true;
            }
            else
            {
                return false;
            }

        }


        private string AppendCompanyAppName(string aPath)
        {
            aPath = Path.Combine(aPath, config.CompanyName);
            aPath = Path.Combine(aPath, config.AppName);
            return aPath;
        }

        public bool TryGetDataFolderPath(out string folder)
        {
            TryDetermineDataFolderPath();

            folder = userDataFolder;

            if (string.IsNullOrWhiteSpace(userDataFolder))
            {
                return false;
            }
            return true;
        }

        public bool TryGetConfigFolderPath(out string folder)
        {
            TryDetermineConfigFolderPath();

            folder = userConfigFolder;

            if (string.IsNullOrWhiteSpace(userConfigFolder))
            {
                return false;
            }
            return true;
        }

        public bool DataFolderExists()
        {
            if (!string.IsNullOrWhiteSpace(userDataFolder))
            {
                if (Directory.Exists(userDataFolder))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ConfigFolderExists()
        {
            if (!string.IsNullOrWhiteSpace(userConfigFolder))
            {
                if (Directory.Exists(userConfigFolder))
                {
                    return true;
                }
            }
            return false;
        }

        private bool TryHandleFolder(string folderPath, bool checkMagicFile = false)
        {
            // If folder already exists: 
            if (Directory.Exists(folderPath))
            {
                // Optionally check for a magic file with a correct GUID
                if (checkMagicFile && !CheckMagicFile(folderPath))
                {
                    return false;
                }

                // Has write access to folder? 
                if (!HasWriteAccess(folderPath))
                {
                    return false;
                }

                return true;
            }
            else
            {   // Folder doesn't exist, try to create it
                if (TryCreateFolder(folderPath))
                {
                    // Has write access to folder? 
                    if (!HasWriteAccess(folderPath))
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// TryCreateUserConfigFolder() handles the user configuration folder. 
        /// This is one of two key methods in this class. 
        /// It investigates if folder already exists, 
        /// Creates it if not. 
        /// Makes sure there is write access to the folder. 
        /// Optionally handles magic files. 
        /// </summary>
        /// <returns>
        /// True if success. False if not. 
        /// </returns>
        public bool TryCreateUserConfigFolder()
        {
            if (TryHandleFolder(UserConfigFolder, !string.IsNullOrWhiteSpace(config.UserConfigMagic)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// TryCreateUserDataFolder() handles the user data folder. 
        /// This is one of two key methods in this class. 
        /// It investigates if folder already exists, 
        /// Creates it if not. 
        /// Makes sure there is write access to the folder. 
        /// Optionally handles magic files. 
        /// </summary>
        /// <returns>
        /// True if success. False if not. 
        /// </returns>

        public bool TryCreateUserDataFolder()
        {
            if (TryHandleFolder(UserDataFolder, !string.IsNullOrWhiteSpace(config.UserDataMagic)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Checks if magic file exists and has correct name as specified in configuration of 
        /// magic strings. 
        /// If it fails an exception is thrown. 
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns>
        /// True if magic file exists and has correct name as specified in magic strings. 
        /// </returns>
        /// <exception cref="InvalidOperationException"></exception>
        private bool CheckMagicFile(string folderPath)
        {
            // if user data folder: 
            if (folderPath == userDataFolder)
            {
                if (File.Exists(magicDataFilePath))
                {
                    return true;
                }
                else
                {
                    string msg = string.Format("Magic file with name {0}\nNot found!", magicDataFilePath);
                    throw new InvalidOperationException(msg);
                }
            }


            // if user config folder: 
            if (folderPath == userConfigFolder)
            {
                if (File.Exists(magicConfigFilePath))
                {
                    return true;
                }
                else
                {
                    string msg = string.Format("Magic file with name {0}\nNot found!", magicConfigFilePath);
                    throw new InvalidOperationException(msg);
                }
            }

            return false;

        }

        /// <summary>
        /// Tries to generate a magic file for the user data folder. 
        /// If the configured magic string is empty it returns false. 
        /// </summary>
        /// <returns>
        /// True if it was successfull. 
        /// False if magic string was empty or operation unsuccessfull. 
        /// </returns>
        public bool TryCreateUserDataMagicFile()
        {
            return TryCreateMagicFile(UserDataFolder);
        }

        /// <summary>
        /// Tries to generate a magic file for the user configuration folder. 
        /// If the configured magic string is empty it returns false. 
        /// </summary>
        /// <returns>
        /// True if it was successfull. 
        /// False if magic string was empty or operation unsuccessfull. 
        /// </returns>
        public bool TryCreateUserConfigMagicFile()
        {
            return TryCreateMagicFile(UserConfigFolder);
        }


        private bool TryCreateMagicFile(string folderPath)
        {
            // For user data folder: 
            if (folderPath == userDataFolder)
            {
                // if magic string is empty, return false, 
                if (string.IsNullOrWhiteSpace(config.UserDataMagic))
                {
                    return false;
                }
                if (TryCreateFileForce(magicDataFilePath))
                {
                    return true;
                }
                return false;
            }

            // For user configuration folder: 
            if (folderPath == userConfigFolder)
            {
                // if magic string is empty, return false, 
                if (string.IsNullOrWhiteSpace(config.UserConfigMagic))
                {
                    return false;
                }
                if (TryCreateFile(magicConfigFilePath))
                {
                    return true;
                }
                return false;
            }

            return false;
        }


        private static bool HasWriteAccess(string folderPath)
        {
            try
            {
                // Attempt to create a temporary file within the directory.
                string tempFilePath = Path.Combine(folderPath, Path.GetRandomFileName());
                if (!File.Exists(tempFilePath))
                {
                    using (FileStream fs = File.Create(tempFilePath))
                    {
                        // The temporary file was created successfully.
                        File.Delete(tempFilePath); // Clean up the temporary file.
                        return true;
                    }
                }
                else
                {
                    // if exists we will run another attemp: 
                    tempFilePath = Path.Combine(folderPath, Path.GetRandomFileName());
                    if (!File.Exists(tempFilePath))
                    {
                        using (FileStream fs = File.Create(tempFilePath))
                        {
                            // The temporary file was created successfully.
                            File.Delete(tempFilePath); // Clean up the temporary file.
                            return true;
                        }
                    } else
                    {
                        return false; 
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // You don't have access to the security information
                return false;
            }
            catch (Exception)
            {
                // Other exceptions, handle accordingly
                return false;
            }

            return false; // No write access found
        }

        private static bool TryCreateFolder(string folderPath)
        {
            try
            {
                Directory.CreateDirectory(folderPath);
                return true; // Folder created successfully
            }
            catch (Exception)
            {
                // Handle folder creation failure, log or throw an exception if necessary
                return false;
            }
        }

        /// <summary>
        /// Tries to create file. 
        /// If it already exists it is overwritten. 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>
        /// True if success. 
        /// False if fails. 
        /// </returns>
        private static bool TryCreateFileForce(string filePath)
        {
            try
            {
                // Attempt to create the file, but don't open it.
                using (File.Create(filePath)) { }
                return true; // File creation was successful.
            }
            catch (Exception)
            {
                // Handle any exceptions that may occur during file creation.
                return false; // File creation failed.
            }
        }


        /// <summary>
        /// Tries to create file. 
        /// Unless it already exists.  
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>
        /// True if success. 
        /// False if fails. 
        /// </returns>
        private static bool TryCreateFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    // Attempt to create the file, but don't open it.
                    using (File.Create(filePath)) { }
                    return true; // File creation was successful.
                }
                return false;
            }
            catch (Exception)
            {
                // Handle any exceptions that may occur during file creation.
                return false; // File creation failed.
            }
        }


    }
}
