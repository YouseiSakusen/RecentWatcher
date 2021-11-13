using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elf.RecentWatcher
{
	public interface IRecentWriter
	{
		public Task<InitialWritingSettings> GetInitialWritingSettingsAsync();

		public Task AddTargetFileAsync(RegistTargetFile targetFile);
	}
}
