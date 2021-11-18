using elf.Windows.Libraries.ShellLinks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace elf.Windows.Libraries;

public class ShellLink : IDisposable
{
	private readonly IShellLinkW? shell;
	private readonly IPersistFile? persist;

	public string GetLinkSourceFilePath(string linkFilePath)
	{
		this.persist?.Load(linkFilePath, 0x00000000);
		var srcPath = new StringBuilder(IShellLinkW.MAX_PATH, IShellLinkW.MAX_PATH);
		var data = new WIN32_FIND_DATAW();

		this.shell?.GetPath(srcPath, srcPath.Capacity, ref data, SLGP_FLAGS.UNCPRIORITY);

		return srcPath.ToString();
	}

	public ShellLink()
	{
		this.shell = (IShellLinkW)new ShellLinkObject();
		this.persist = this.shell as IPersistFile;
	}

	private bool disposedValue;

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

	public void Dispose()
	{
		// このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
