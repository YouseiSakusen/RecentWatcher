using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace elf.Windows.Libraries.ShellLinks;


// !!!!!!! 注意 !!!!!!! コメントはGoogle翻訳等の結果を管理人が意訳した内容なので
// ******************** 間違えている可能性も十二分にあります。鵜呑みにはしないでください！

/// <summary>取得したファイルの情報を表します。</summary>
[CLSCompliant(false), StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
internal struct WIN32_FIND_DATAW
{
	public uint dwFileAttributes;
	public FILETIME ftCreationTime;
	public FILETIME ftLastAccessTime;
	public FILETIME ftLastWriteTime;
	public uint nFileSizeHigh;
	public uint nFileSizeLow;
	public uint dwReserved0;
	public uint dwReserved1;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = IShellLinkW.MAX_PATH)]
	public string cFileName;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
	public string cAlternateFileName;
}

/// <summary>取得するパス情報のタイプを指定するフラグを表します。</summary>
[CLSCompliant(false), Flags]
internal enum SLGP_FLAGS : uint
{
	SHORTPATH = 1,
	UNCPRIORITY = 2,
	RAWPATH = 4,
}

/// <summary>Windowの表示スタイルを表す列挙型。</summary>
internal enum SW : int
{
	HIDE = 0,
	NORMAL = 1,
	SHOWNORMAL = 1,
	SHOWMINIMIZED = 2,
	MAXIMIZE = 3,
	SHOWMAXIMIZED = 3,
	SHOWNOACTIVATE = 4,
	SHOW = 5,
	MINIMIZE = 6,
	SHOWMINNOACTIVE = 7,
	SHOWNA = 8,
	RESTORE = 9,
	SHOWDEFAULT = 10,
	FORCEMINIMIZE = 11,
}

/// <summary>リンク先を探す場合の方法を設定するフラグ。</summary>
[CLSCompliant(false), Flags]
internal enum SLR_FLAGS : uint
{
	/// <summary>
	/// <para>このフラグを設定するとリンク先が解決できない場合にダイアログを表示しません。</para>
	/// <para>リンクを解決するタイムアウトはミリ秒で指定しタイムアウト期間内に
	/// 解決できない場合は関数を終了します。
	/// タイムアウトが設定されていない場合のデフォルト値は3,000ミリ秒（3秒）です。</para>
	/// </summary>
	NO_UI = 1,
	/// <summary>
	/// リンク先に近い一致を許可します。Windows ME/2000 以降では効果がないため
	/// 別のフラグを使用します。
	/// </summary>
	ANY_MATCH = 2,
	/// <summary>
	/// <para>このフラグを設定するとリンク先が変更された場合、ショートカットファイル内の
	/// 情報を更新します。</para>
	/// <para>このフラグを設定した場合は、IPersistFile::IsDirtyを呼び出して
	/// リンクオブジェクトを変更する必要はありません</para>
	/// </summary>
	UPDATE = 4,
	/// <summary>このフラグを設定するとリンク情報を更新しません。</summary>
	NOUPDATE = 8,
	/// <summary>リンク先を自動で探さない場合に指定します。</summary>
	NOSEARCH = 16,
	/// <summary>リンク先を追跡しません。</summary>
	NOTRACK = 32,
	/// <summary>
	/// <para>分散リンク先追跡を無効にします。</para>
	/// <para>デフォルトではドライブ文字が変更されたリムーバブルメディアでも
	/// ボリューム名に基づいて複数デバイスにまたがってUNCパスを追跡しますが、
	/// このフラグが設定されている場合はその追跡を無効にします</para>
	/// </summary>
	NOLINKINFO = 64,
	/// <summary>Microsoft Windows Installerを呼び出します。</summary>
	INVOKE_MSI = 128,

	/// <summary>
	/// SDKには文書化されていません。NO_UIと同じだと予想しますが、
	/// hWndの無いアプリケーションが対象です。
	/// </summary>
	UI_WITH_MSG_PUMP = 0x101,
}
