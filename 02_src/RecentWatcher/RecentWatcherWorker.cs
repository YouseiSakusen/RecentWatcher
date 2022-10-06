namespace RecentWatcher;

/// <summary>�ŋߎg�����t�@�C���t�H���_���Ď����郏�[�J�N���X��\���܂��B</summary>
public class RecentWatcherWorker : BackgroundService
{
	/// <summary>���������s���܂��B</summary>
	/// <param name="stoppingToken">�����̌p���E���~��\��CancellationToken�B</param>
	/// <returns>���������s����Task�B</returns>
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

	/// <summary>�R���X�g���N�^�B</summary>
	/// <param name="workerLogger">���O���o�͂���ILogger<RecentWatcherWorker>�B�iDI�R���e�i����C���W�F�N�V�����j</param>
	/// <param name="recentFileWatcher">�ŋߎg�����t�@�C���t�H���_���Ď�����RecentFileWatcher�B�iDI�R���e�i����C���W�F�N�V�����j</param>
	public RecentWatcherWorker(ILogger<RecentWatcherWorker> workerLogger, RecentFileWatcher recentFileWatcher)
		=> (this.logger, this.watcher) = (workerLogger, recentFileWatcher);
}