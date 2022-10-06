using elf.Windows.Libraries;

namespace RecentWatcher;

/// <summary>最近使ったファイルフォルダを監視します。</summary>
public class RecentFileWatcher : IDisposable
{
	private FileSystemWatcher? watcher = null;
	private InitialWriteSettings? settings = null;

	/// <summary>最近使ったファイルフォルダの監視を開始します。</summary>
	public async ValueTask StartAsync()
	{
		this.settings = await this.editor.GetInitialWriteSettingsAsync();
		var recent = Environment.GetFolderPath(Environment.SpecialFolder.Recent);

		// 未登録のファイルがあれば登録してFileSystemWatcherを初期化
		await this.writeUnregistedFilesAsync(recent);
		this.initializedFileSystemWatcher(recent);
	}

	/// <summary>未登録の最近使ったファイルを保存します。</summary>
	/// <param name="recentPath">最近使ったファイルフォルダのパスを表す文字列。</param>
	/// <returns>未登録の最近使ったファイルを保存するValueTask。</returns>
	private async ValueTask writeUnregistedFilesAsync(string recentPath)
	{
		var recentDir = new DirectoryInfo(recentPath);

		await using (var scope = this.scopeFactory.CreateAsyncScope())
		{
			var shellLink = scope.ServiceProvider.GetRequiredService<ShellLink>();

			foreach (var item in recentDir.EnumerateFiles("*.lnk"))
				await this.writeRecentFile(item.FullName, this.settings!.LatestRecentDateTime, shellLink);
		}
	}

	/// <summary>最近使ったファイルを保存します。</summary>
	/// <param name="linkFilePath">ショートカットファイルのパスを表す文字列。</param>
	/// <param name="minDateTime">
	/// <para>このパラメータに指定した最小日時をショートカットファイルの最終更新日時が
	/// 超えたファイルのみ保存します。</para>
	/// <para>nullを指定した場合は常にlinkFilePathパラメータに指定したショートカットファイルの
	/// リンク先ファイルが保存されます</para>
	/// </param>
	/// <param name="shellLink">ショートカットファイルの情報を取得するShellLink。</param>
	/// <returns>最近使ったファイルを保存するValueTask。</returns>
	private async ValueTask writeRecentFile(string linkFilePath, DateTime? minDateTime, ShellLink shellLink)
	{
		var linkFile = new FileInfo(linkFilePath);

		if (minDateTime.HasValue)
		{
			if (linkFile.LastWriteTime <= minDateTime)
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
		this.watcher.Created += async (object sender, FileSystemEventArgs e) =>
		{
			await using (var scope = this.scopeFactory.CreateAsyncScope())
			{
				var shellLink = scope.ServiceProvider.GetRequiredService<ShellLink>();

				await this.writeRecentFile(e.FullPath, null, shellLink).ConfigureAwait(false);
			}
		};
		this.watcher.EnableRaisingEvents = true;
	}

	private readonly IRecentFileEditor editor;
	private readonly ILogger<RecentFileWatcher> logger;
	private readonly IServiceScopeFactory scopeFactory;

	/// <summary>コンストラクタ。</summary>
	/// <param name="recentFileEditor">最近使ったファイルを読み書きするIRecentFileEditor。（DIコンテナからインジェクション）</param>
	/// <param name="watcherLogger">ログを出力するILogger<RecentFileWatcher>。（DIコンテナからインジェクション）</param>
	public RecentFileWatcher(IRecentFileEditor recentFileEditor, ILogger<RecentFileWatcher> watcherLogger, IServiceScopeFactory factory)
		=> (this.editor, this.logger, this.scopeFactory) = (recentFileEditor, watcherLogger, factory);

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