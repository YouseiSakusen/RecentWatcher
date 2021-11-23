using Dapper;
using elf.DataAccesses.Interfaces;
using System.Text;

namespace RecentWatcher;

/// <summary>最近使ったファイルDBの読み書きを表します。</summary>
public class RecentFileEditor : IRecentFileEditor
{
	/// <summary>最近使ったファイルをDBに追加します。</summary>
	/// <param name="targetFile">登録対象のファイルを表すRegistTargetFile。</param>
	/// <returns>最近使ったファイルをDBに追加するTask。</returns>
	public async Task AddTargetFileAsync(RegistTargetFile targetFile)
	{
		await using (var connection = await this.connectionFactory.GetConnectionAsync())
		{
			var tran = await connection.BeginTransactionAsync();

			try
			{
				// 存在する場合はInsertしない
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

	/// <summary>同一更新日時のファイルを取得するSelect文を取得します。</summary>
	/// <returns>同一更新日時のファイルを取得するSelect文を表す文字列。</returns>
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

	/// <summary>対象ファイルのInsert文を取得します。</summary>
	/// <returns>対象ファイルのInsert文を表す文字列。</returns>
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

	/// <summary>初回書き込み設定を取得します。</summary>
	/// <returns>初回書き込み設定を取得するTask。</returns>
	public async Task<InitialWriteSettings> GetInitialWriteSettingsAsync()
	{
		await using (var connection = await this.connectionFactory.GetConnectionAsync())
		{
			var ret = await connection.QueryFirstAsync<InitialWriteSettings>(this.getLatestRecentFileAccessDateTime());

			ret.Extensions.AddRange(await connection.QueryAsync<string>(this.getExtensionSelectSql()));

			return ret;
		}
	}

	/// <summary>対象ファイルの拡張子を取得するSelect文を取得します。</summary>
	/// <returns>対象ファイルの拡張子を取得するSelect文を表す文字列。</returns>
	private string getExtensionSelectSql()
	{
		var sql = new StringBuilder();
		sql.AppendLine(" SELECT ");
		sql.AppendLine(" 	* ");
		sql.AppendLine(" FROM ");
		sql.AppendLine(" 	Extensions ");

		return sql.ToString();
	}

	/// <summary>最も新しいアクセス日時を取得するSelect文を取得します。</summary>
	/// <returns>最も新しいアクセス日時を取得するSelect文を表す文字列。</returns>
	private string getLatestRecentFileAccessDateTime()
	{
		var sql = new StringBuilder();
		sql.AppendLine(" SELECT ");
		sql.AppendLine(" 	coalesce(max(RH.AccessTime), '0001/01/01 00:00:01') AS AccessTime ");
		sql.AppendLine(" FROM ");
		sql.AppendLine(" 	RecentHistories RH ");

		return sql.ToString();
	}

	private readonly IDapperConnectionFactory connectionFactory;

	/// <summary>コンストラクタ。</summary>
	/// <param name="dapperConnectionFactory">Dapperを使用するためのDB接続を取得するIDapperConnectionFactory。</param>
	public RecentFileEditor(IDapperConnectionFactory dapperConnectionFactory)
		=> this.connectionFactory = dapperConnectionFactory;
}
