using elf.Repositoris;
using System;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace elf.Repositoris.Tests
{
	public class SqliteDapperAccessTest
	{
		[Fact()]
		public async Task GetAsyncConnectionTest()
		{
			var dbPath = @"D:\MyDocuments\GitHubRepositories\RecentWatcher\src\elf.DataAccess.SqliteTest\bin\Debug\net5.0\SqliteDapperTest.db";
			var connection = await new SqliteDapperAccess(dbPath).GetAsyncConnection();

			Assert.True(connection.State == System.Data.ConnectionState.Open);
		}

		[Fact(DisplayName = "DBパス間違い")]
		public async Task GetAsyncConnectionWrongPathTest()
		{
			var dbPath = @"D:\MyDocuments\GitHubRepositories\RecentWatcher\src\elf.DataAccess.SqliteTest\bin\Debug\net5.0\hoge.db";
			var connection = await new SqliteDapperAccess(dbPath).GetAsyncConnection();

			
			Assert.Throws<SQLiteException>(() =>
			{
				var command = new SQLiteCommand("SELECT * FROM Test", connection as SQLiteConnection);
				var ret = command.ExecuteScalar();
			});
		}

		[Fact(DisplayName = "コンストラクタ")]
		public void SqliteDapperAccessConstructorTest()
		{
			var da = new SqliteDapperAccess("DataSourcePath");

			Assert.True(da.DbConnectString == @"data source=DataSourcePath");
		}
	}
}
