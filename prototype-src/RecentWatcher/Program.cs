using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using elf.Repositoris;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace elf.RecentWatcher
{
	public class Program
	{
		public static async Task<int> Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			var logger = host.Services.GetRequiredService<ILogger<Program>>();

			try
			{
				await host.RunAsync();
				
				return 0;
			}
			catch (Exception ex)
			{
				logger.LogError(ex.ToString());
				return -1;
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureServices((hostContext, services) =>
				{
					var logPath = Path.Combine(AppContext.BaseDirectory, "Debug.log");
					//var dbPath = Path.GetDirectoryName(AppContext.BaseDirectory);
					var dbPath = Path.Combine(AppContext.BaseDirectory, hostContext.Configuration.GetSection("SqliteFileName").Value);
					var logger = new TextLogger(logPath);
					logger.Write($"CreateHostBuilder - {Environment.GetFolderPath(Environment.SpecialFolder.Recent)}");
					//logger.Write($"{args[0]}");

					services.AddSingleton<TextLogger>(logger);
					services.AddHostedService<RecentWatcherWorker>();
					services.AddSingleton<IDapperAccess>(new SqliteDapperAccess(dbPath));
					services.AddSingleton<IRecentWriter, RecentWriter>();
					services.AddSingleton<RecentWatcher>();
				})
				.UseWindowsService();
	}
}
