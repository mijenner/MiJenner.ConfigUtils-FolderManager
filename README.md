# MiJenner.ConfigUtils-FolderManager

Desktop Folder Manager in cross platform C# for handling applications storage of user data and user configuration data. 
The system enables creation of a subfolder structure with `.../YourCompany/YourApp/ ...` where you can store data and or configuration files. 
On Windows the path could be: 
`
C:\Users\john\AppData\Roaming\YourCompany\YourApp 
`
On MacOS the path could be: 
`
/Users/john/.config/YourCompany/YourApp 
`

# Getting Started 
## Add using 
Include the namespace: 
```cs
using MiJenner.ConfigUtils;
```

## Create configuration object 
Next, use the configuration builder to build a configuration object: 
```cs
var config = new DesktopFolderManagerConfigBuilder()
    .WithUserDataPolicy(UserDataPolicy.PolicyFileAppDataRoaming)
    .WithUserDataMagic("")
    .WithUserConfigPolicy(UserConfigPolicy.PolicyFileAppDataRoaming)
    .WithUserConfigMagic("")
    .WithCompanyAndAppName("YourCompany", "YourApp")
    .Build();
```

### Policies 
UserDataPolicy and UserConfigPolicy choose from the same list of the following enums / locations (if username is john): 
* ```PolicyFileAppDataLocal```: Windows C:\Users\john\AppData\Local, MacOS: /Users/john/.local/share, Linux: todo 
* ```PolicyFileAppDataRoaming```: Windows C:\Users\john\AppData\Roaming, MacOS: /Users/john/.config, Linux: todo  
* ```PolicyFileDesktop```: Windows C:\Users\john\Desktop, MacOS: /Users/john/Desktop, Linux: todo  
* ```PolicyFileDocument```: Windows C:\Users\john\Documents, MacOS: /Users/john/, Linux: todo 
* ```PolicyFileDontUse```: Do not use storage in this location.  

Note: the latter may not be possible due to security settings on Windows. 

## Create FolderManager instance 
Next, you will typically create a FolderManager instance, based on the just created configuration: 

```cs
var folderManager = new DesktopFolderManager(config);
```

## Try to create folders 
And you would use some logic to determine proper folders for users data and for users configuration, which may be the same. And once found, you want to create them (if already existing this will not harm content). For user data this could be something like: 
```cs
string dataFolder;
if (!folderManager.TryGetDataFolderPath(out dataFolder))
{
    // user data folder path could not be determined. 
    // logic to get out of that situation.
}
else
{
    // try to create user data folder: 
    if (!folderManager.TryCreateUserDataFolder())
    {
        // user configuration folder could not be created. 
        // logic to get out of that situation. 
    }
}
```

And if it differs for user configuration data you could use: 
```cs
string configFolder;
if (!folderManager.TryGetConfigFolderPath(outconfigFolder)) 
{
    // configuration folder path could not be determined. 
    // logic to get out of that situation.
}
else
{
    // try to create user configuration folder: 
    if (!folderManager.TryCreateUserConfigFolder())
    {
        // user configuration folder could not be created. 
        // logic to get out of that situation. 
    }
}
```

Now your strings ```cs dataFolder``` and ```cs configFolder``` are (hopefully) proper paths to where you want to store user data, sqlite file, json files etc and so on. 

# NuGet package 
There is a NuGet package available for easy usage.
