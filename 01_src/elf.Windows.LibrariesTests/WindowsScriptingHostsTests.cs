using Xunit;
using System.Reflection;
using System.IO;

namespace elf.Windows.Libraries.Tests
{
	public class WindowsScriptingHostsTests
	{
		[Theory(DisplayName ="ショートカットファイルあり")]
		[InlineData(@"D:\MyVideo\現実主義勇者の王国再建記 11話 「李代桃僵（りだいとうきょう）」.mp4", @"現実主義勇者の王国再建記 11話 「李代桃僵（りだいとうきょう）」.mp4.lnk")]
		[InlineData(@"D:\MyVideo\平穏世代の韋駄天達 #03 「飄」.mp4", @"平穏世代の韋駄天達 #03 「飄」.mp4.lnk")]
		[InlineData(@"D:\MyVideo\ヴァニタスの手記 Mémoire 04 「Bal masqué―仮面が嗤う夜―」.mp4", @"ヴァニタスの手記 Mémoire 04 「Bal masqué―仮面が嗤う夜―」.mp4.lnk")]
		public void GetSourcePathFromShortcutFileTest(string srcPath, string linkFileName)
		{
			var linkFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			linkFilePath = Path.Combine(linkFilePath!, linkFileName);

			var gettedSrcPath = WindowsScriptingHosts.GetSourcePathFromShortcutFile(linkFilePath);
			Assert.True(WindowsScriptingHosts.GetSourcePathFromShortcutFile(linkFilePath) == srcPath);
		}
	}
}