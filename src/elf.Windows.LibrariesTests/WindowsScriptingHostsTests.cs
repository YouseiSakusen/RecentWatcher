using Xunit;
using System.Reflection;
using System.IO;

namespace elf.Windows.Libraries.Tests
{
	public class WindowsScriptingHostsTests
	{
		[Theory(DisplayName ="ショートカットファイルあり")]
		[InlineData(@"D:\MyVideo\Cat - 78698.mp4", @"Cat - 78698.mp4.lnk")]
		[InlineData(@"D:\MyVideo\Cat - 79034é.mp4", @"Cat - 79034é.mp4.lnk")]
		public void GetSourcePathFromShortcutFileTest(string srcPath, string linkFileName)
		{
			var linkFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			linkFilePath = Path.Combine(linkFilePath!, linkFileName);

			var gettedSrcPath = WindowsScriptingHosts.GetSourcePathFromShortcutFile(linkFilePath);
			Assert.True(WindowsScriptingHosts.GetSourcePathFromShortcutFile(linkFilePath) == srcPath);
		}
	}
}