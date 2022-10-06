namespace RecentWatcher;

/// <summary>初期書き込み設定を表します。</summary>
public class InitialWriteSettings
{
	/// <summary>対象の拡張子を取得・設定します。</summary>
	public List<string> Extensions { get; set; } = new List<string>();

	/// <summary>
	/// 直近のファイル日時を取得・設定します。
	/// </summary>
	public DateTime? LatestRecentDateTime { get; set; } = null;
}
