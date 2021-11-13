using Xunit;
using elf.Windows.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elf.Windows.Libraries.Tests
{
	public class WindowsScriptingHostsTests
	{
		[Fact(DisplayName ="ショートカットファイルが存在する場合")]
		public void GetShortcutFileTargetPathTest()
		{
			Assert.True(WindowsScriptingHosts.GetShortcutFileTargetPath(@"D:\MyDocuments\GitHubRepositories\RecentWatcher\src\elf.Windows.Libraries.Tests\RecentWatcher.sln.lnk") == @"D:\MyDocuments\GitHubRepositories\RecentWatcher\src\RecentWatcher\RecentWatcher.sln");
		}

		[Fact(DisplayName = "ショートカットファイルが存在しない場合")]
		public void GetNewShortcutFileTargetPathTest()
		{
			Assert.True(WindowsScriptingHosts.GetShortcutFileTargetPath(@"D:\MyDocuments\GitHubRepositories\RecentWatcher\src\elf.Windows.Libraries.Tests\Recent.sln.lnk") == string.Empty);
		}
	}
}