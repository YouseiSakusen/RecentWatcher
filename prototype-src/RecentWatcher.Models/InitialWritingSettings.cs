using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elf.RecentWatcher
{
	public class InitialWritingSettings
	{
		public List<string> Extensions { get; set; } = new List<string>();

		public DateTime? LatestRecentDateTime { get; set; } = null;

		public string RecentFolderPath { get; set; } = string.Empty;
	}
}
