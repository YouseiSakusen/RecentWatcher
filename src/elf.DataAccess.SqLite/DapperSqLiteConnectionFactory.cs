using elf.DataAccesses.Interfaces;
using System.Data.Common;
using System.Data.SQLite;

namespace elf.DataAccess.SqLite;

/// <summary>DapperからSQLiteに接続するDbConnectionのファクトリを表します。</summary>
public class DapperSqLiteConnectionFactory : IDapperConnectionFactory
{
	/// <summary>DBへの接続文字列を取得します。</summary>
	public string DbConnectString
		=> this.connectString;

	/// <summary>DbConnectionを取得します。</summary>
	/// <returns>取得したDbConnection。</returns>
	public async Task<DbConnection> GetConnectionAsync()
	{
		var connection = new SQLiteConnection(this.connectString);

		await connection.OpenAsync();

		return connection;
	}

	private readonly string connectString;

	/// <summary>コンストラクタ。</summary>
	/// <param name="filePath">SQLiteデータベースファイルへのフルパスを表す文字列。</param>
	public DapperSqLiteConnectionFactory(string filePath)
		=> this.connectString = new SQLiteConnectionStringBuilder() { DataSource = filePath }.ToString();
}