using System;

namespace elf.Windows.Libraries
{
	public static class WindowsScriptingHosts
	{
		public static string GetShortcutFileTargetPath(string shortcutFilePath)
		{
			var wshType = Type.GetTypeFromProgID("WScript.Shell");
			dynamic shell = Activator.CreateInstance(wshType);
			
			return shell.CreateShortcut(shortcutFilePath).TargetPath;
		}
	}
}
