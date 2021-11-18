using Xunit;
using elf.Windows.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace elf.Windows.Libraries.Tests
{
	public class ShellLinkTests
	{
		[Theory(DisplayName = "ショートカットファイルあり")]
		[InlineData(@"H:\出会って5秒でバトル\出会って5秒でバトル 第07話 「暴君」 (WebRip 1920x1080 HEVC AAC EMBER).mkv", @"出会って5秒でバトル 第07話 「暴君」 (WebRip 1920x1080 HEVC AAC EMBER).mkv.lnk")]
		[InlineData(@"H:\ヴァニタスの手記\ヴァニタスの手記 Mémoire 04 「Bal masqué―仮面が嗤う夜―」 (WebRip 1920x1080 HEVC AAC EMBER).mkv", @"ヴァニタスの手記 Mémoire 04 「Bal masqué―仮面が嗤う夜―」 (WebRip 1920x1080 HEVC AAC EMBER).mkv.lnk")]
		public void GetLinkSourceFilePathTest(string srcPath, string linkFileName)
		{
			var linkFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			linkFilePath = Path.Combine(linkFilePath!, linkFileName);

			using (var shellLink = new ShellLink())
			{
				var sourceFilePath = shellLink.GetLinkSourceFilePath(linkFilePath);
				Assert.True(sourceFilePath == srcPath);
			}
		}
	}
}