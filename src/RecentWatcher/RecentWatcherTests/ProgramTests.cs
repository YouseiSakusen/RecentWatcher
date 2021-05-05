using Microsoft.VisualStudio.TestTools.UnitTesting;
using elf.RecentWatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace elf.RecentWatcher.Tests
{
	[TestClass()]
	public class ProgramTests
	{
		[TestMethod()]
		public void MainTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void CreateHostBuilderTest()
		{
			var args = new string[] { };

			var host = Program.CreateHostBuilder(args).Build();
			
			Assert.Fail();
		}
	}
}