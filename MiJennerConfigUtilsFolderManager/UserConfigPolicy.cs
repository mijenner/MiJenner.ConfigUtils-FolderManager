namespace MiJenner.ConfigUtils
{
    public enum UserConfigPolicy
    {
        Unknown, 
        PolicyFileDocument,
        PolicyFileAppDataLocal,
        PolicyFileAppDataRoaming,
        PolicyFileDesktop,
        /// <summary>
        /// Root of the user profile folder, typically C:\Users\Username.
        /// Useful as a fallback when Documents and AppData are restricted by IT policy.
        /// </summary>
        PolicyFileUserProfile,
        /// <summary>
        /// System temporary folder, typically C:\Users\Username\AppData\Local\Temp.
        /// Almost always writable. Use as a last resort — data may be cleared by the system.
        /// </summary>
        PolicyFileTempPath,
    }
}
