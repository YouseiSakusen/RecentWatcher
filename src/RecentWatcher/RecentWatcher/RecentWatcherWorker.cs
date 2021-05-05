using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace elf.RecentWatcher
{
	public class RecentWatcherWorker : BackgroundService
	{
		public override Task StartAsync(CancellationToken cancellationToken)
		{

			return base.StartAsync(cancellationToken);
		}

		public override Task StopAsync(CancellationToken cancellationToken)
		{

			return base.StopAsync(cancellationToken);
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			this.logger.LogInformation("Recent Watcher Service Started");
			//using var fileSystemWatcher = new FileSystemWatcher(Environment.GetFolderPath(Environment.SpecialFolder.Recent));
			this.logger.LogInformation($"Db File Name: {this.config.GetValue<string>("SqliteFileName")}");


			var tcs = new TaskCompletionSource<bool>();
			stoppingToken.Register(t => (t as TaskCompletionSource<bool>).SetResult(true), tcs);
			await tcs.Task;

			this.logger.LogInformation("Recent Watcher Service Stopped");
		}

		private readonly ILogger<RecentWatcherWorker> logger;
		private readonly IConfiguration config;

		public RecentWatcherWorker(ILogger<RecentWatcherWorker> logg, IConfiguration conf)
		{
			this.logger = logg;
			this.config = conf;
		}
	}
}
