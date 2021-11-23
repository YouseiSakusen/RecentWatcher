namespace RecentWatcher;

public class RecentWatcherWorker : BackgroundService
{
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		this.watcher.StartAsync();

		var tcs = new TaskCompletionSource<bool>();
		stoppingToken.Register(s => (s as TaskCompletionSource<bool>)?.SetResult(true), tcs);
		await tcs.Task;

		logger.LogInformation($"{nameof(RecentWatcherWorker)} Finished!");
	}

	private readonly ILogger<RecentWatcherWorker> logger;
	private readonly RecentFileWatcher watcher;

	public RecentWatcherWorker(ILogger<RecentWatcherWorker> workerLogger, RecentFileWatcher recentFileWatcher)
		=> (this.logger, this.watcher) = (workerLogger, recentFileWatcher);
}