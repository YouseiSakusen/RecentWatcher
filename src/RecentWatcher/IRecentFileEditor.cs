namespace RecentWatcher;

/// <summary>最近使ったファイルのDB読み書き処理を表します。</summary>
public interface IRecentFileEditor
{
	/// <summary>初回書き込み設定を取得します。</summary>
	/// <returns>初回書き込み設定を表すInitialWriteSettings。</returns>
	public Task<InitialWriteSettings> GetInitialWriteSettingsAsync();

	/// <summary>最近使ったファイルをDBに追加します。</summary>
	/// <param name="targetFile">登録対象のファイルを表すRegistTargetFile。</param>
	/// <returns>最近使ったファイルをDBに追加するTask。</returns>
	public Task AddTargetFileAsync(RegistTargetFile targetFile);
}
