using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elf.Repositoris
{
	public class SqliteDapperAccess : IDapperAccess
	{
		public string DbConnectString => this.connectString;

		public async Task<DbConnection> GetAsyncConnection()
		{
			this.connection = new SQLiteConnection(this.connectString);
			await this.connection.OpenAsync();

			return this.connection;
		}

		private readonly string connectString = string.Empty;
		private DbConnection connection = null;

		public SqliteDapperAccess(string filePath)
		{
			this.connectString = new SQLiteConnectionStringBuilder() { DataSource = filePath }.ToString();
		}
	}
}
