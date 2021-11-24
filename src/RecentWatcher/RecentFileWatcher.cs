using elf.DataAccesses.Interfaces;
using elf.Windows.Libraries;

namespace RecentWatcher;

/// <summary>最近使ったファイルフォルダを監視します。</summary>
public class RecentFileWatcher : IDisposable
{
	private FileSystemWatcher? watcher = null;
	private InitialWriteSettings? settings = null;

	/// <summary>最近使ったファイルフォルダの監視を開始します。</summary>
	public async Task StartAsync()
	{
		this.settings = await this.editor.GetInitialWriteSettingsAsync();
		var recent = Environment.GetFolderPath(Environment.SpecialFolder.Recent);

		// 未登録のファイルがあれば登録してFileSystemWatcherを初期化
		await this.writeUnregistedFiles(recent)
			.ContinueWith(t => this.initializedFileSystemWatcher(recent));
	}

	private async Task writeUnregistedFiles(string recentPath)
	{
		var recentDir = new DirectoryInfo(recentPath);

		using (var shellLink = new ShellLink())
		{
			foreach (var item in recentDir.EnumerateFiles("*.lnk"))
				await this.writeRecentFile(item.FullName, this.settings!.LatestRecentDateTime, shellLink);
		}
	}

	private async Task writeRecentFile(string linkFilePath, DateTime? minDateTime, ShellLink shellLink)
	{
		var linkFile = new FileInfo(linkFilePath);

		if (minDateTime.HasValue)
		{
			if (linkFile.LastAccessTime <= minDateTime)
				return;
		}

		var realPath = shellLink.GetLinkSourceFilePath(linkFilePath);

		if (this.settings!.Extensions.Any(e => e == Path.GetExtension(realPath)))
			await this.editor.AddTargetFileAsync(new RegistTargetFile(linkFile.LastWriteTime, realPath));
	}

	/// <summary>FileSystemWatcherを初期化します。</summary>
	/// <param name="recentFolderPath">最近使ったファイルフォルダのフルパスを表す文字列。</param>
	private void initializedFileSystemWatcher(string recentFolderPath)
	{
		this.watcher = new FileSystemWatcher(recentFolderPath);
		this.watcher.Created += (object sender, FileSystemEventArgs e) =>
		{
			using (var shellLink = new ShellLink())
			{
				Console.WriteLine(shellLink.GetLinkSourceFilePath(e.FullPath));
			}
		};
		this.watcher.EnableRaisingEvents = true;
	}

	private IRecentFileEditor editor;

	public RecentFileWatcher(IRecentFileEditor recentFileEditor)
		=> this.editor = recentFileEditor;

	private bool disposedValue;

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
				this.watcher?.Dispose();

			// TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
			// TODO: 大きなフィールドを null に設定します
			disposedValue = true;
		}
	}

	// // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
	// ~RecentFileWatcher()
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