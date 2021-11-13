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

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			await this.textLogger.WriteAsync("End ExecuteAsync");

			await base.StopAsync(cancellationToken);
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			this.logger.LogInformation("Recent Watcher Service Started");
			await this.textLogger.WriteAsync($"Start ExecuteAsync - {Environment.GetFolderPath(Environment.SpecialFolder.Recent)}");
			await this.watcher.StartAsync();

			var tcs = new TaskCompletionSource<bool>();
			stoppingToken.Register(t => (t as TaskCompletionSource<bool>).SetResult(true), tcs);
			await tcs.Task;

			this.logger.LogInformation("Recent Watcher Service Stopped");
		}

		private readonly ILogger<RecentWatcherWorker> logger;
		private readonly RecentWatcher watcher;
		private readonly TextLogger textLogger;

		public RecentWatcherWorker(ILogger<RecentWatcherWorker> logg, RecentWatcher recentWatcher, TextLogger tlogger)
		{
			this.logger = logg;
			this.watcher = recentWatcher;
			this.textLogger = tlogger;
		}
	}
}
