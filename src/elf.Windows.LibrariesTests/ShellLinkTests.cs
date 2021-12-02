using Xunit;
using System.IO;
using System.Reflection;

namespace elf.Windows.Libraries.Tests
{
	public class ShellLinkTests
	{
		[Theory(DisplayName = "ショートカットファイルあり")]
		[InlineData(@"D:\MyVideo\Cat - 78698.mp4", @"Cat - 78698.mp4.lnk")]
		[InlineData(@"D:\MyVideo\Cat - 79034é.mp4", @"Cat - 79034é.mp4.lnk")]
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