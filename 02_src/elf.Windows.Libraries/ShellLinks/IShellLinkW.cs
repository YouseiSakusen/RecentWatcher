using System.Runtime.InteropServices;
using System.Text;

[assembly: CLSCompliant(false)]
namespace elf.Windows.Libraries.ShellLinks;

/// <summary>
/// シェルリンクのプロパティにアクセスするためのすべてのメソッドと、ショートカットファイルを読み取るためのヘルパーメソッドを提供します。
/// </summary>
[CLSCompliant(false), ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214F9-0000-0000-C000-000000000046")]
internal interface IShellLinkW
{
	internal const int MAX_PATH = 260;

	/// <summary>
	/// シェルリンクオブジェクトのパスとファイル名を取得します。
	/// </summary>
	/// <param name="pszFile"></param>
	/// <param name="cch"></param>
	/// <param name="pfd"></param>
	/// <param name="fFlags"></param>
	//HRESULT GetPath([out, size_is(cch)] LPWSTR pszFile, [in] int cch, [in, out, ptr] WIN32_FIND_DATAW *pfd, [in] DWORD fFlags);
	void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cch, ref WIN32_FIND_DATAW pfd, SLGP_FLAGS fFlags);

	/// <summary>
	/// シェルリンクのリストを取得します。
	/// </summary>
	/// <param name="ppidl"></param>
	//HRESULT GetIDList([out] LPITEMIDLIST * ppidl);
	void GetIDList(out IntPtr ppidl);

	/// <summary>
	/// シェルリンクのリストを設定します。
	/// </summary>
	/// <param name="pidl"></param>
	//HRESULT SetIDList([in] LPCITEMIDLIST pidl);
	void SetIDList(IntPtr pidl);

	/// <summary>
	/// シェルリンクの説明文字列を取得します。
	/// </summary>
	/// <param name="pszName"></param>
	/// <param name="cch"></param>
	//HRESULT GetDescription([out, size_is(cch)] LPWSTR pszName, int cch);
	void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cch);

	/// <summary>
	/// シェルリンクの説明文字列を設定します。
	/// </summary>
	/// <param name="pszName"></param>
	//HRESULT SetDescription([in] LPCWSTR pszName);
	void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

	/// <summary>
	/// シェルリンクの作業ディレクトリ名を取得します。
	/// </summary>
	/// <param name="pszDir"></param>
	/// <param name="cch"></param>
	//HRESULT GetWorkingDirectory([out, size_is(cch)] LPWSTR pszDir, int cch);
	void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cch);

	/// <summary>
	/// シェルリンクの作業ディレクトリ名を設定します。
	/// </summary>
	/// <param name="pszDir"></param>
	//HRESULT SetWorkingDirectory([in] LPCWSTR pszDir);
	void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

	/// <summary>
	/// シェルリンクのコマンドライン引数を取得します。
	/// </summary>
	/// <param name="pszArgs"></param>
	/// <param name="cch"></param>
	//HRESULT GetArguments([out, size_is(cch)] LPWSTR pszArgs, int cch);
	void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cch);

	/// <summary>
	/// シェルリンクのコマンドライン引数を設定します。
	/// </summary>
	/// <param name="pszArgs"></param>
	//HRESULT SetArguments([in] LPCWSTR pszArgs);
	void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

	/// <summary>
	/// シェルリンクのホットキーを取得します。
	/// </summary>
	/// <param name="pwHotkey"></param>
	//HRESULT GetHotkey([out] WORD *pwHotkey);
	void GetHotkey(out ushort pwHotkey);

	/// <summary>
	/// シェルリンクのホットキーを設定します。
	/// </summary>
	/// <param name="wHotkey"></param>
	//HRESULT SetHotkey([in] WORD wHotkey);
	void SetHotkey(ushort wHotkey);

	/// <summary>
	/// シェルリンクの表示コマンドを取得します。
	/// </summary>
	/// <param name="piShowCmd"></param>
	//HRESULT GetShowCmd([out] int *piShowCmd);
	void GetShowCmd(out SW piShowCmd);

	/// <summary>
	/// シェルリンクの表示コマンドを設定します。
	/// </summary>
	/// <param name="iShowCmd"></param>
	//HRESULT SetShowCmd([in] int iShowCmd);
	void SetShowCmd(SW iShowCmd);

	/// <summary>
	/// シェルリンクアイコンの場所（パスとインデックス）を取得します 。
	/// </summary>
	/// <param name="pszIconPath"></param>
	/// <param name="cch"></param>
	/// <param name="piIcon"></param>
	//HRESULT GetIconLocation([out, size_is(cch)] LPWSTR pszIconPath, [in] int cch, [out] int *piIcon);
	void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cch, out int piIcon);

	/// <summary>
	/// シェルリンクアイコンの場所（パスとインデックス）を設定します 。
	/// </summary>
	/// <param name="pszIconPath"></param>
	/// <param name="iIcon"></param>
	//HRESULT SetIconLocation([in] LPCWSTR pszIconPath, [in] int iIcon);
	void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

	/// <summary>
	/// シェルリンクの相対パスを設定します。
	/// </summary>
	/// <param name="pszPathRel"></param>
	/// <param name="dwReserved"></param>
	//HRESULT SetRelativePath([in] LPCWSTR pszPathRel, [in] DWORD dwReserved);
	void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, uint dwReserved);

	/// <summary>
	/// シェルリンクオブジェクトを検索してシェルリンクを解決します。
	/// </summary>
	/// <param name="hwnd"></param>
	/// <param name="fFlags"></param>
	//HRESULT Resolve([in] HWND hwnd, [in] DWORD fFlags);
	void Resolve(IntPtr hwnd, SLR_FLAGS fFlags);

	/// <summary>
	/// シェルリンクパスとファイル名を設定します。
	/// </summary>
	/// <param name="pszFile"></param>
	//HRESULT SetPath([in] LPCWSTR pszFile);
	void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
}