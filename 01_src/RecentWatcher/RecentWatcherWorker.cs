namespace RecentWatcher;

/// <summary>最近使ったファイルフォルダを監視するワーカクラスを表します。</summary>
public class RecentWatcherWorker : BackgroundService
{
	/// <summary>処理を実行します。</summary>
	/// <param name="stoppingToken">処理の継続・中止を表すCancellationToken。</param>
	/// <returns>処理を実行するTask。</returns>
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		try
		{
			await this.watcher.StartAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			this.logger.LogError(ex, ex.ToString());
			throw;
		}

		var tcs = new TaskCompletionSource<bool>();
		stoppingToken.Register(s => (s as TaskCompletionSource<bool>)?.SetResult(true), tcs);
		await tcs.Task;

		logger.LogInformation($"{nameof(RecentWatcherWorker)} Finished!");
	}

	private readonly ILogger<RecentWatcherWorker> logger;
	private readonly RecentFileWatcher watcher;

	/// <summary>コンストラクタ。</summary>
	/// <param name="workerLogger">ログを出力するILogger<RecentWatcherWorker>。（DIコンテナからインジェクション）</param>
	/// <param name="recentFileWatcher">最近使ったファイルフォルダを監視するRecentFileWatcher。（DIコンテナからインジェクション）</param>
	public RecentWatcherWorker(ILogger<RecentWatcherWorker> workerLogger, RecentFileWatcher recentFileWatcher)
		=> (this.logger, this.watcher) = (workerLogger, recentFileWatcher);
}