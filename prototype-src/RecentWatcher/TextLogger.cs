using System;
using System.IO;
using System.Threading.Tasks;

namespace elf.RecentWatcher
{
	public class TextLogger
	{
		public void Write(string message)
			=> File.AppendAllTextAsync(this.filePath, $"{DateTime.Now:yyyy/MM/dd HH:mm:ss} {message + Environment.NewLine}");

		public async Task WriteAsync(string message)
			=> await File.AppendAllTextAsync(this.filePath, $"{DateTime.Now:yyyy/MM/dd HH:mm:ss} {message + Environment.NewLine}");

		private readonly string filePath;

		public TextLogger(string logPath)
			=> this.filePath = logPath;
	}
}
