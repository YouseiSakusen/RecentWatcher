namespace RecentWatcher;

public class RecentWatcherWorker : BackgroundService
{
    private readonly ILogger<RecentWatcherWorker> _logger;

    public RecentWatcherWorker(ILogger<RecentWatcherWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // ‚±‚±‚ÉÀs‚·‚éˆ—‚ğ‹Lq

        var tcs = new TaskCompletionSource<bool>();
        stoppingToken.Register(s => (s as TaskCompletionSource<bool>)?.SetResult(true), tcs);
        await tcs.Task;

        _logger.LogInformation($"{nameof(RecentWatcherWorker)} Finished!");
    }
}