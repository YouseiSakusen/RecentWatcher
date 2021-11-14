﻿namespace RecentWatcher;

/// <summary>最近使ったファイルフォルダを監視します。</summary>
public class RecentFileWatcher : IDisposable
{
	private FileSystemWatcher? watcher = null;

	/// <summary>最近使ったファイルフォルダの監視を開始します。</summary>
	public void StartAsync() =>
		// FileSystemWatcherを初期化
		this.initializedFileSystemWatcher(Environment.GetFolderPath(Environment.SpecialFolder.Recent));

	/// <summary>FileSystemWatcherを初期化します。</summary>
	/// <param name="recentFolderPath">最近使ったファイルフォルダのフルパスを表す文字列。</param>
	private void initializedFileSystemWatcher(string recentFolderPath)
	{
		// FileSystemWatcherを初期化
		this.watcher = new FileSystemWatcher(recentFolderPath);
		this.watcher.Created += (object sender, FileSystemEventArgs e)
			=> Console.WriteLine(e.FullPath);
		this.watcher.EnableRaisingEvents = true;
	}

	public string getSourceFilePath(string linkFilePath)
	{
		var linkFile = new FileInfo(linkFilePath);
		//var realPath = WindowsScriptingHosts.GetShortcutFileTargetPath(linkFilePath);
	}

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