using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace elf.RecentWatcher
{
	public class Program
	{
		public static async Task<int> Main(string[] args)
		{
			try
			{
				await CreateHostBuilder(args).Build().RunAsync();

				return 0;
			}
			catch (Exception)
			{

				return -1;
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureServices((hostContext, services) =>
				{
					services.AddHostedService<RecentWatcherWorker>();
				})
				.UseWindowsService();
	}
}
