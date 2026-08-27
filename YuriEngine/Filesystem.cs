using System.Reflection;

namespace YuriEngine;

public class Filesystem{
	public static string AppDataPath(){
		string AssemblyName = Assembly.GetEntryAssembly()?.GetName().Name ?? throw new("Anonymous assembly could not find data path");
		if (OperatingSystem.IsLinux())
		{
			string UserName = Environment.UserName;
			return $"/home/{UserName}/.config/{AssemblyName}";
		}
		else if (OperatingSystem.IsWindows())
		{
			return $"{Environment.ExpandEnvironmentVariables("$APPDATA")}/{AssemblyName}";
		}
		throw new PlatformNotSupportedException();
    }
}
