namespace RecentWatcher;

/// <summary>DBへ登録するファイルを表します。</summary>
public class RegistTargetFile
{
	/// <summary>ファイルの最終更新日時を取得・設定します。</summary>
	public DateTime? RealAccessDateTime { get; set; } = null;

	/// <summary>ファイルの最終更新日時からミリ秒を除いたDateTimeを取得します。</summary>
	public DateTime? AccessTime
	{
		get
		{
			if (this.RealAccessDateTime.HasValue)
			{
				return new DateTime(this.RealAccessDateTime.Value.Year,
									this.RealAccessDateTime.Value.Month,
									this.RealAccessDateTime.Value.Day,
									this.RealAccessDateTime.Value.Hour,
									this.RealAccessDateTime.Value.Minute,
									this.RealAccessDateTime.Value.Second);
			}
			else
			{
				return null;
			}
		}
	}

	/// <summary>ファイルのフルパスを取得・設定します。</summary>
	public string FilePath { get; set; } = string.Empty;

	/// <summary>コンストラクタ。</summary>
	/// <param name="accessTime">ファイルの更新日時を表すDateTime?。</param>
	/// <param name="filePath">ファイルのフルパスを表す文字列。</param>
	public RegistTargetFile(DateTime? accessTime, string filePath)
		=> (this.RealAccessDateTime, this.FilePath) = (accessTime, filePath);
}
