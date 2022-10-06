using elf.Windows.Libraries.ShellLinks;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace elf.Windows.Libraries;

/// <summary>WindowsのShellLinkを表します。</summary>
public class ShellLink : IDisposable
{
	private readonly IShellLinkW? shell;
	private readonly IPersistFile? persist;

	/// <summary>ショートカットファイルからリンク先ファイルのパスを取得します。</summary>
	/// <param name="linkFilePath">ショートカットファイルのパスを表す文字列。</param>
	/// <returns>ショートカットファイルから取得したリンク先ファイルのパスを表す文字列。</returns>
	public string GetLinkSourceFilePath(string linkFilePath)
	{
		if (!File.Exists(linkFilePath))
			return string.Empty;

		this.persist?.Load(linkFilePath, 0x00000000);
		var srcPath = new StringBuilder(IShellLinkW.MAX_PATH, IShellLinkW.MAX_PATH);
		var data = new WIN32_FIND_DATAW();

		this.shell?.GetPath(srcPath, srcPath.Capacity, ref data, SLGP_FLAGS.UNCPRIORITY);

		return srcPath.ToString();
	}

	/// <summary>デフォルトコンストラクタ。</summary>
	public ShellLink()
	{
		this.shell = (IShellLinkW)new ShellLinkObject();
		this.persist = this.shell as IPersistFile;
	}

	private bool disposedValue;

	/// <summary>このクラスのインスタンスを破棄します。</summary>
	/// <param name="disposing">Disposeが実行されたかを表すbool。</param>
	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				if (this.persist != null)
					Marshal.ReleaseComObject(this.persist);
				if (this.shell != null)
					Marshal.ReleaseComObject(this.shell);
			}

			// TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
			// TODO: 大きなフィールドを null に設定します
			disposedValue = true;
		}
	}

	// // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
	// ~ShellLink()
	// {
	//     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
	//     Dispose(disposing: false);
	// }

	/// <summary>このクラスのインスタンスを破棄します。</summary>
	public void Dispose()
	{
		// このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
