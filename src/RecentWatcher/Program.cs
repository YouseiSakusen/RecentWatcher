using elf.DataAccess.SqLite;
using elf.DataAccesses.Interfaces;
using RecentWatcher;

IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureServices((hostContext, services) =>
	{
		var dbPath = Path.Combine(AppContext.BaseDirectory, hostContext.Configuration.GetSection("SqliteFileName").Value);

		services.AddHostedService<RecentWatcherWorker>()
			.AddSingleton<RecentFileWatcher>()
			.AddSingleton<IDapperConnectionFactory>(new DapperSqLiteConnectionFactory(dbPath))
			.AddTransient<RecentFileEditor>();
	})
	.Build();

await host.RunAsync();
