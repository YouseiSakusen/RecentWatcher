using elf.DataAccess.SqLite;
using elf.DataAccesses.Interfaces;
using elf.Windows.Libraries;
using Microsoft.Extensions.Logging.EventLog;
using RecentWatcher;

IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureLogging(logging =>
	{
		logging.AddEventLog(config =>
		{
			config.SourceName = "Recent File Watcher Source";
			config.LogName = "Recent File Watcher";
		})
		.AddFilter<EventLogLoggerProvider>(level => LogLevel.Information <= level);
	})
	.ConfigureServices((hostContext, services) =>
	{
		var dbPath = Path.Combine(AppContext.BaseDirectory, hostContext.Configuration.GetSection("SqliteFileName").Value);

		services.AddHostedService<RecentWatcherWorker>()
			.AddSingleton<RecentFileWatcher>()
			.AddSingleton<IDapperConnectionFactory>(new DapperSqLiteConnectionFactory(dbPath))
			.AddTransient<IRecentFileEditor, RecentFileEditor>()
			.AddScoped<ShellLink>();
	})
	.Build();

await host.RunAsync();
