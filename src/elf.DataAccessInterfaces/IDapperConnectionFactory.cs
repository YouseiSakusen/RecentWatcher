using System.Data.Common;

namespace elf.DataAccesses.Interfaces;

/// <summary>DapperでDBへアクセスするためのインタフェースを表します。</summary>
public interface IDapperConnectionFactory
{
	/// <summary>
	/// DbConnectionを取得します。
	/// </summary>
	/// <returns>取得したDbConnection。</returns>
	public Task<DbConnection> GetConnectionAsync();

	/// <summary>DBへの接続文字列を取得します。</summary>
	public string DbConnectString { get; }
}
