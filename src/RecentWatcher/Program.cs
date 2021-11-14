using RecentWatcher;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<RecentWatcherWorker>();
    })
    .Build();

await host.RunAsync();
