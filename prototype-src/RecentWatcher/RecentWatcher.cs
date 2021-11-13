using elf.Windows.Libraries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elf.RecentWatcher
{
	public class RecentWatcher : IDisposable
	{
		private FileSystemWatcher watcher = null;
		private InitialWritingSettings settings = null;

		public async Task StartAsync()
		{
			this.settings = await this.writer.GetInitialWritingSettingsAsync();

			var recent = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
			if (!string.IsNullOrEmpty(recent))
				this.settings.RecentFolderPath = recent;
			await this.textLogger.WriteAsync($"Start {nameof(StartAsync)} - {this.settings.RecentFolderPath}");

			// 未登録のファイルがあれば登録してFileSystemWatcherを初期化
			await this.writeUnregistedFiles()
				.ContinueWith(t => this.initializedFileSystemWatcher());
		}

		private async Task writeUnregistedFiles()
		{
			var recentDir = new DirectoryInfo(this.settings.RecentFolderPath);
			await this.textLogger.WriteAsync($"初期書き込み前");
			await this.textLogger.WriteAsync($"this.settings.RecentFolderPath - {this.settings.RecentFolderPath}");
			await this.textLogger.WriteAsync($"recentDir - {recentDir.FullName}");

			foreach (var item in recentDir.EnumerateFiles("*.lnk"))
				await this.writeToRecentFile(item.FullName, this.settings.LatestRecentDateTime);

			await this.textLogger.WriteAsync($"初期書き込み完了");
		}

		private void initializedFileSystemWatcher()
		{
			// FileSystemWatcherを初期化
			this.watcher = new FileSystemWatcher(this.settings.RecentFolderPath);
			this.watcher.Created += async (object sender, FileSystemEventArgs e) => await this.writeToRecentFile(e.FullPath, null);
			this.watcher.EnableRaisingEvents = true;
		}

		private async Task writeToRecentFile(string linkFilePath, DateTime? minDateTime)
		{
			var linkFile = new FileInfo(linkFilePath);

			if (minDateTime.HasValue)
			{
				if (linkFile.LastAccessTime <= minDateTime)
					return;
			}

			var realPath = WindowsScriptingHosts.GetShortcutFileTargetPath(linkFilePath);
			await this.textLogger.WriteAsync($"{nameof(writeToRecentFile)} - ショートカットファイルのパス({realPath})");

			if (this.settings.Extensions.Any(e => e == Path.GetExtension(realPath)))
				await this.writer.AddTargetFileAsync(new RegistTargetFile(linkFile.LastWriteTime, realPath));
		}

		private IRecentWriter writer;
		private TextLogger textLogger;

		public RecentWatcher(IRecentWriter recentWriter, TextLogger tlogger)
		{
			this.writer = recentWriter;
			this.textLogger = tlogger;
		}

		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					this.watcher?.Dispose();
				}

				// TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
				// TODO: 大きなフィールドを null に設定します
				disposedValue = true;
			}
		}

		// // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
		// ~RecentWatcher()
		// {
		//     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
