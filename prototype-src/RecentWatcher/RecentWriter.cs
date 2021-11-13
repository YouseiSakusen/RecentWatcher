using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using elf.Repositoris;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace elf.RecentWatcher
{
	public class RecentWriter : IRecentWriter
	{
		public async Task<InitialWritingSettings> GetInitialWritingSettingsAsync()
		{
			await using (var connection = await this.dapperAccess.GetAsyncConnection())
			{
				try
				{
					var ret = await connection.QueryFirstAsync<InitialWritingSettings>(this.getRecentFolderPathSelectSql(), new { PropertyName = "RecentFolderPath" });

					ret.Extensions.AddRange(await connection.QueryAsync<string>(this.getExtensionSelectSql()));
					ret.LatestRecentDateTime = await connection.QueryFirstOrDefaultAsync<DateTime?>(this.getLatestRecentFileAccessDateTime());

					return ret;
				}
				catch (Exception ex)
				{

					throw;
				}
			}
		}

		private string getExtensionSelectSql()
		{
			var sql = new StringBuilder();
			sql.AppendLine(" SELECT ");
			sql.AppendLine(" 	* ");
			sql.AppendLine(" FROM ");
			sql.AppendLine(" 	Extensions ");

			return sql.ToString();
		}

		private string getLatestRecentFileAccessDateTime()
		{
			var sql = new StringBuilder();
			sql.AppendLine(" SELECT ");
			sql.AppendLine(" 	coalesce(max(RH.AccessTime), '0001/01/01 00:00:01') AS AccessTime ");
			sql.AppendLine(" FROM ");
			sql.AppendLine(" 	RecentHistories RH ");

			return sql.ToString();
		}

		private string getRecentFolderPathSelectSql()
		{
			var sql = new StringBuilder();
			sql.AppendLine(" SELECT ");
			sql.AppendLine("     PRP.PropertyValue AS RecentFolderPath ");
			sql.AppendLine(" FROM ");
			sql.AppendLine("     Properties PRP ");
			sql.AppendLine(" WHERE ");
			sql.AppendLine("     PRP.PropertyName = :PropertyName ");

			return sql.ToString();
		}

		public async Task AddTargetFileAsync(RegistTargetFile targetFile)
		{
			await using(var connection = await this.dapperAccess.GetAsyncConnection())
			{
				var tran = await connection.BeginTransactionAsync();

				try
				{
					// 存在する場合は抜ける
					//var temp = await connection.ExecuteScalarAsync<DateTime?>(this.getSameAccessTimeRecord(), new { AccessTime = targetFile.AccessTime });
					//if (!temp.HasValue)
					if (!(await connection.ExecuteScalarAsync<DateTime?>(this.getSameAccessTimeRecord(), new { AccessTime = targetFile.AccessTime })).HasValue)
						await connection.ExecuteAsync(this.getAddTargetFileSql(), targetFile);

					await tran.CommitAsync();
				}
				catch (Exception)
				{
					await tran.RollbackAsync();
					throw;
				}
			}
		}

		private string getSameAccessTimeRecord()
		{
			var sql = new StringBuilder();
			sql.AppendLine(" SELECT ");
			sql.AppendLine("	AccessTime ");
			sql.AppendLine(" FROM ");
			sql.AppendLine("	RecentHistories ");
			sql.AppendLine(" WHERE ");
			sql.AppendLine(" 	AccessTime = :AccessTime ");

			return sql.ToString();
		}

		private string getAddTargetFileSql()
		{
			var sql = new StringBuilder();
			sql.AppendLine(" INSERT INTO RecentHistories ");
			sql.AppendLine(" ( ");
			sql.AppendLine(" 	  AccessTime ");
			sql.AppendLine(" 	, FilePath ");
			sql.AppendLine(" ) VALUES ( ");
			sql.AppendLine(" 	  :AccessTime ");
			sql.AppendLine(" 	, :FilePath ");
			sql.AppendLine(" ) ");

			return sql.ToString();
		}

		private readonly ILogger<RecentWriter> logger;
		private readonly IDapperAccess dapperAccess;
		private readonly string recentPath = Environment.GetFolderPath(Environment.SpecialFolder.Recent);

		public RecentWriter(ILogger<RecentWriter> logg, IDapperAccess da)
		{
			this.logger = logg;
			this.dapperAccess = da;
		}
	}
}
