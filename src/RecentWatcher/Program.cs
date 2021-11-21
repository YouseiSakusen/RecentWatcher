using RecentWatcher;

IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureServices(services =>
	{
		services.AddHostedService<RecentWatcherWorker>()
			.AddTransient<RecentFileWatcher>();
	})
	.Build();

await host.RunAsync();
